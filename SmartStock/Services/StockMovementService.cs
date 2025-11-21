using Microsoft.EntityFrameworkCore;
using SmartStock.Data;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Enums;
using SmartStock.Responses;

namespace SmartStock.Services
{
    public class StockMovementService
    {
        private readonly DataContext _context;

        public StockMovementService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<StockMovement>>> FindAll(StockMovementFiltersDto? filters)
        {
            var page = filters?.Page ?? 1;
            var pageSize = filters?.PageSize ?? 20;

            var query = _context.StockMovements
                .AsNoTracking()
                .OrderBy(p => p.CreatedAt);

            var stockMovements = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ApiResponse<List<StockMovement>>
            {
                Message = "Movimentações de estoque listadas com sucesso.",
                Data = stockMovements
            };
        }

        public async Task<ApiResponse<StockMovement>> FindOne(int id)
        {
            var stockMovement = await _context.StockMovements
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (stockMovement is null)
                throw new InvalidOperationException("Movimentação de estoque não encontrada.");

            return new ApiResponse<StockMovement>
            {
                Message = "Movimentação de estoque encontrada com sucesso.",
                Data = stockMovement
            };
        }

        public async Task<ApiResponse<StockMovement>> CreateStockMovement(CreateStockMovementDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var product = await _context.Products.FindAsync(dto.ProductId);

            if (product is null)
                throw new InvalidOperationException("Produto não encontrado.");

            ApplyStockChange(product, dto.Type, dto.QuantityChange);

            var stockMovement = new StockMovement
            {
                ProductId = dto.ProductId,
                QuantityChange = dto.QuantityChange,
                Type = dto.Type,
            };

            if (dto.OrderId.HasValue)
            {
                var orderExists = await _context.Orders
                .AsNoTracking()
                .AnyAsync(o => o.Id == dto.OrderId.Value);

                if (!orderExists)
                    throw new InvalidOperationException("Pedido não encontrado.");

                stockMovement.OrderId = dto.OrderId.Value;
            }

            _context.StockMovements.Add(stockMovement);
            await _context.SaveChangesAsync();

            return new ApiResponse<StockMovement>
            {
                Message = "Movimentação de estoque criada com sucesso.",
                Data = stockMovement
            };
        }

        public async Task<ApiResponse<StockMovement>> UpdateStockMovement(int id, UpdateStockMovementDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var stockMovement = await _context.StockMovements
                .Include(sm => sm.Product)
                .FirstOrDefaultAsync(sm => sm.Id == id);

            if (stockMovement is null)
                throw new InvalidOperationException("Movimentação de estoque não encontrada.");

            var mustRecalculateStock =
                dto.ProductId.HasValue ||
                dto.QuantityChange.HasValue ||
                dto.Type.HasValue;

            if (mustRecalculateStock)
            {
                ApplyStockChange(stockMovement.Product, stockMovement.Type, -stockMovement.QuantityChange);

                if (dto.ProductId.HasValue && dto.ProductId.Value != stockMovement.ProductId)
                {
                    var newProduct = await _context.Products.FindAsync(dto.ProductId.Value)
                                   ?? throw new InvalidOperationException("Produto não encontrado.");

                    stockMovement.ProductId = dto.ProductId.Value;
                    stockMovement.Product = newProduct;
                }

                if (dto.QuantityChange.HasValue)
                    stockMovement.QuantityChange = dto.QuantityChange.Value;

                if (dto.Type.HasValue)
                    stockMovement.Type = dto.Type.Value;

                ApplyStockChange(stockMovement.Product, stockMovement.Type, stockMovement.QuantityChange);
            }

            if (dto.OrderId.HasValue)
            {
                var orderExists = await _context.Orders
                    .AsNoTracking()
                    .AnyAsync(o => o.Id == dto.OrderId.Value);

                if (!orderExists)
                    throw new InvalidOperationException("Pedido não encontrado.");

                stockMovement.OrderId = dto.OrderId.Value;
            }

            await _context.SaveChangesAsync();

            return new ApiResponse<StockMovement>
            {
                Message = "Movimentação de estoque atualizada com sucesso.",
                Data = stockMovement
            };
        }

        public async Task<ApiResponse<StockMovement>> DeleteStockMovement(int id)
        {
            var stockMovement = await _context.StockMovements.FindAsync(id);

            if (stockMovement is null)
                throw new InvalidOperationException("Movimentação de estoque não encontrada.");

            _context.StockMovements.Remove(stockMovement);
            await _context.SaveChangesAsync();

            return new ApiResponse<StockMovement>
            {
                Message = "Movimentação de estoque removida com sucesso.",
                Data = stockMovement
            };
        }

        private void ApplyStockChange(Product product, StockMovementType type, int quantityChange)
        {
            var stockVariation = type == StockMovementType.ENTRY
                ? quantityChange
                : -quantityChange;

            var newStock = product.StockQuantity + stockVariation;

            if (newStock < 0)
                throw new InvalidOperationException("Estoque insuficiente para essa movimentação.");

            product.StockQuantity = newStock;
        }
    }
}
