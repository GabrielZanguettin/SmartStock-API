using Microsoft.EntityFrameworkCore;
using SmartStock.Data;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Responses;

namespace SmartStock.Services
{
    public class OrderItemService
    {
        private readonly DataContext _context;

        public OrderItemService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<OrderItem>>> FindAll(OrderItemFiltersDto? filters)
        {
            var page = filters?.Page ?? 1;
            var pageSize = filters?.PageSize ?? 20;

            var query = _context.OrderItems
                .AsNoTracking()
                .OrderBy(p => p.Id);

            var orderItems = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ApiResponse<List<OrderItem>>
            {
                Message = "Os itens dos pedidos foram listados com sucesso.",
                Data = orderItems
            };
        }

        public async Task<ApiResponse<OrderItem>> FindOne(int id)
        {
            var orderItem = await _context.OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (orderItem is null)
                throw new InvalidOperationException("Item não encontrado.");

            return new ApiResponse<OrderItem>
            {
                Message = "Item encontrado com sucesso.",
                Data = orderItem
            };
        }

        public async Task<ApiResponse<OrderItem>> CreateOrderItem(CreateOrderItemDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var orderExists = await _context.Orders
                .AsNoTracking()
                .AnyAsync(s => s.Id == dto.OrderId);

            if (!orderExists)
                throw new InvalidOperationException("Pedido não encontrado.");

            var product = await _context.Products.FindAsync(dto.ProductId);

            if (product is null)
                throw new InvalidOperationException("Produto não encontrado.");

            var orderItem = new OrderItem
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                ProductName = product.Name,
                UnitPrice = product.UnitPrice,
            };

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<OrderItem>
            {
                Message = "O item foi adicionado ao pedido com sucesso.",
                Data = orderItem
            };
        }

        public async Task<ApiResponse<OrderItem>> UpdateOrderItem(int id, UpdateOrderItemDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem is null)
                throw new InvalidOperationException("Item não encontrado.");

            if (dto.Quantity.HasValue)
            {
                orderItem.Quantity = dto.Quantity.Value;
            }

            if (dto.OrderId.HasValue)
            {
                var orderExists = await _context.Orders
                    .AsNoTracking()
                    .AnyAsync(s => s.Id == dto.OrderId.Value);

                if (!orderExists)
                    throw new InvalidOperationException("Pedido não encontrado.");

                orderItem.OrderId = dto.OrderId.Value;
            }

            if (dto.ProductId.HasValue)
            {
                var product = await _context.Products.FindAsync(dto.ProductId);

                if (product is null)
                    throw new InvalidOperationException("Produto não encontrado.");
                    
                orderItem.Product = product;
                orderItem.ProductName = product.Name;
                orderItem.UnitPrice = product.UnitPrice;
            }

            await _context.SaveChangesAsync();

            return new ApiResponse<OrderItem>
            {
                Message = "Item atualizado com sucesso.",
                Data = orderItem
            };
        }

        public async Task<ApiResponse<OrderItem>> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem is null)
                throw new InvalidOperationException("Item do pedido não encontrado.");

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<OrderItem>
            {
                Message = "Item removido do pedido com sucesso.",
                Data = orderItem
            };
        }
    }
}
