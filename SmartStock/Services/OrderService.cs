using Microsoft.EntityFrameworkCore;
using SmartStock.Data;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Enums;
using SmartStock.Responses;

namespace SmartStock.Services
{
    public class OrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<Order>>> FindAll(OrderFiltersDto? filters)
        {
            var page = filters?.Page ?? 1;
            var pageSize = filters?.PageSize ?? 20;

            var query = _context.Orders
                .AsNoTracking()
                .OrderBy(p => p.Id);

            var orders = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ApiResponse<List<Order>>
            {
                Message = "Pedidos listados com sucesso.",
                Data = orders
            };
        }

        public async Task<ApiResponse<Order>> FindOne(int id)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (orders is null)
                throw new InvalidOperationException("Pedido não encontrado.");

            return new ApiResponse<Order>
            {
                Message = "Item do pedido encontrado com sucesso.",
                Data = orders
            };
        }

        public async Task<ApiResponse<Order>> CreateOrder(CreateOrderDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var order = new Order
            {
                CustomerName = dto.CustomerName,
                Discount = dto.Discount,
                Taxes = dto.Taxes,
                Status = OrderStatus.DRAFT,
                CreatedAt = DateTime.UtcNow,
            };

            foreach (var itemDto in dto.Items)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId)
                              ?? throw new InvalidOperationException("Produto não encontrado.");

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.UnitPrice
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new ApiResponse<Order>
            {
                Message = "O pedido foi criado com sucesso.",
                Data = order
            };
        }

        public async Task<ApiResponse<Order>> UpdateOrder(int orderId, UpdateOrderDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new InvalidOperationException("Pedido não encontrado.");

            if (dto.CustomerName is not null)
                order.CustomerName = dto.CustomerName;

            if (dto.Discount.HasValue)
                order.Discount = dto.Discount.Value;

            if (dto.Taxes.HasValue)
                order.Taxes = dto.Taxes.Value;

            if (dto.ToRemove != null && dto.ToRemove.Any())
                RemoveOrderItems(order, dto.ToRemove);

            if (dto.ToEdit != null && dto.ToEdit.Any())
                await EditOrderItemsAsync(order, dto.ToEdit);

            if (dto.ToAdd != null && dto.ToAdd.Any())
                await AddOrderItemsAsync(order, dto.ToAdd);

            await _context.SaveChangesAsync();

            return new ApiResponse<Order>
            {
                Message = "O pedido foi atualizado com sucesso.",
                Data = order
            };
        }

        public async Task<ApiResponse<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order is null)
                throw new InvalidOperationException("Pedido não encontrado.");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return new ApiResponse<Order>
            {
                Message = "Pedido excluído com sucesso.",
                Data = order
            };
        }
        private void RemoveOrderItems(
            Order order,
            IEnumerable<UpdateOrderItemToRemoveDto> itemsToRemove)
        {
            foreach (var removeDto in itemsToRemove)
            {
                var existingItem = order.Items
                    .FirstOrDefault(i => i.Id == removeDto.OrderItemId);

                if (existingItem is null)
                {
                    throw new InvalidOperationException(
                        $"Item do pedido {removeDto.OrderItemId} não encontrado."
                    );
                }

                order.Items.Remove(existingItem);
                _context.OrderItems.Remove(existingItem);
            }
        }
        private async Task EditOrderItemsAsync(
            Order order,
            IEnumerable<UpdateOrderItemToEditDto> itemsToEdit)
        {
            foreach (var editDto in itemsToEdit)
            {
                var existingItem = order.Items
                    .FirstOrDefault(i => i.Id == editDto.OrderItemId);

                if (existingItem is null)
                {
                    throw new InvalidOperationException(
                        $"Item do pedido {editDto.OrderItemId} não encontrado."
                    );
                }

                if (editDto.ProductId.HasValue)
                {
                    var product = await _context.Products.FindAsync(editDto.ProductId.Value)
                        ?? throw new InvalidOperationException(
                            $"Produto {editDto.ProductId} não encontrado."
                        );

                    existingItem.ProductId = product.Id;
                    existingItem.ProductName = product.Name;
                    existingItem.UnitPrice = product.UnitPrice;
                }

                if (editDto.Quantity.HasValue)
                {
                    existingItem.Quantity = editDto.Quantity.Value;
                }
            }
        }
        private async Task AddOrderItemsAsync(Order order, IEnumerable<UpdateOrderItemToAddDto> itemsToAdd)
        {
            foreach (var addDto in itemsToAdd)
            {
                var product = await _context.Products.FindAsync(addDto.ProductId)
                    ?? throw new InvalidOperationException(
                        $"Produto {addDto.ProductId} não encontrado."
                    );

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.UnitPrice,
                    Quantity = addDto.Quantity
                });
            }
        }
    }
}