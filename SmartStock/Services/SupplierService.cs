using Microsoft.EntityFrameworkCore;
using SmartStock.Data;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Responses;

namespace SmartStock.Services
{
    public class SupplierService
    {
        private readonly DataContext _context;

        public SupplierService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<Supplier>>> FindAll(SupplierFiltersDto? filters)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Supplier>> FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Supplier>> CreateSupplier(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Supplier>> UpdateSupplier(int id, Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Supplier>> DeleteSupplier(int id)
        {
            throw new NotImplementedException();
        }
    }
}
