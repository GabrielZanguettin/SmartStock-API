using Microsoft.EntityFrameworkCore;
using SmartStock.Data;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Responses;

namespace SmartStock.Services
{
    public class ProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<Product>>> FindAll(ProductFiltersDto? filters)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Product>> FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Product>> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Product>> UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Product>> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
