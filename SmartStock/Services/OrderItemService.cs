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
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<OrderItem>> FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<OrderItem>> CreateOrderItem(OrderItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<OrderItem>> UpdateOrderItem(int id, OrderItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<OrderItem>> DeleteOrderItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
