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
            var page = filters?.Page ?? 1;
            var pageSize = filters?.PageSize ?? 20;

            var query = _context.Suppliers
                .AsNoTracking()
                .Include(s => s.Products)
                .OrderBy(p => p.Name);

            var suppliers = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ApiResponse<List<Supplier>>
            {
                Message = "Fornecedores listados com sucesso.",
                Data = suppliers
            };
        }

        public async Task<ApiResponse<Supplier>> FindOne(int id)
        {
            var supplier = await _context.Suppliers
                .AsNoTracking()
                .Include(s => s.Products)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (supplier is null)
                throw new InvalidOperationException("Fornecedor não encontrado.");

            return new ApiResponse<Supplier>
            {
                Message = "Fornecedor encontrado com sucesso.",
                Data = supplier
            };
        }

        public async Task<ApiResponse<Supplier>> CreateSupplier(CreateSupplierDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var name = dto.Name.Trim();

            var supplier = new Supplier
            {
                Name = name
            };

            if (dto.Document is not null)
                supplier.Document = dto.Document.Trim();

            if (dto.Phone is not null)
                supplier.Phone = dto.Phone.Trim();

            if (dto.Email is not null)
                supplier.Email = dto.Email.Trim();

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return new ApiResponse<Supplier>
            {
                Message = "Fornecedor criado com sucesso.",
                Data = supplier
            };
        }

        public async Task<ApiResponse<Supplier>> UpdateSupplier(int id, UpdateSupplierDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier is null)
                throw new InvalidOperationException("Fornecedor não encontrado.");

            if (dto.Name is not null)
                supplier.Name = dto.Name.Trim();

            if (dto.Document is not null)
                supplier.Document = dto.Document.Trim();

            if (dto.Phone is not null)
                supplier.Phone = dto.Phone.Trim();

            if (dto.Email is not null)
                supplier.Email = dto.Email.Trim();

            await _context.SaveChangesAsync();

            return new ApiResponse<Supplier>
            {
                Message = "Fornecedor atualizado com sucesso.",
                Data = supplier
            };
        }

        public async Task<ApiResponse<Supplier>> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier is null)
                throw new InvalidOperationException("Fornecedor não encontrado.");

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return new ApiResponse<Supplier>
            {
                Message = "Fornecedor removido com sucesso.",
                Data = supplier
            };
        }
    }
}
