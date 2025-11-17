using Microsoft.EntityFrameworkCore;
using SmartStock.Data;
using SmartStock.Dtos;
using SmartStock.Entities;
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
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<StockMovement>> FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<StockMovement>> CreateStockMovement(StockMovement movement)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<StockMovement>> UpdateStockMovement(int id, StockMovement movement)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<StockMovement>> DeleteStockMovement(int id)
        {
            throw new NotImplementedException();
        }
    }
}
