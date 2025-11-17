using Microsoft.EntityFrameworkCore;
using SmartStock.Data;
using SmartStock.Dtos;
using SmartStock.Entities;
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
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Order>> FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Order>> CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Order>> UpdateOrder(int id, Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Order>> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
