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
            var page = filters?.Page ?? 1;
            var pageSize = filters?.PageSize ?? 20;

            var query = _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Name);

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ApiResponse<List<Product>>
            {
                Message = "Produtos listados com sucesso.",
                Data = products
            };
        }

        public async Task<ApiResponse<Product>> FindOne(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                throw new InvalidOperationException("Produto não encontrado.");

            return new ApiResponse<Product>
            {
                Message = "Produto encontrado com sucesso.",
                Data = product
            };
        }

        public async Task<ApiResponse<Product>> CreateProduct(CreateProductDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var name = dto.Name.Trim();

            var supplierExists = await _context.Suppliers
                .AsNoTracking()
                .AnyAsync(s => s.Id == dto.SupplierId);

            if (!supplierExists)
                throw new InvalidOperationException("Fornecedor não encontrado.");

            var product = new Product
            {
                Name = name,
                UnitPrice = dto.UnitPrice,
                StockQuantity = dto.StockQuantity,
                SupplierId = dto.SupplierId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ApiResponse<Product>
            {
                Message = "Produto criado com sucesso.",
                Data = product
            };
        }

        public async Task<ApiResponse<Product>> UpdateProduct(int id, UpdateProductDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var product = await _context.Products.FindAsync(id);

            if (product is null)
                throw new InvalidOperationException("Produto não encontrado.");

            if (dto.Name is not null)
                product.Name = dto.Name.Trim();

            if (dto.UnitPrice.HasValue)
                product.UnitPrice = dto.UnitPrice.Value;

            if (dto.SupplierId.HasValue)
            {
                var supplierExists = await _context.Suppliers
                    .AsNoTracking()
                    .AnyAsync(s => s.Id == dto.SupplierId.Value);

                if (!supplierExists)
                    throw new InvalidOperationException("Fornecedor não encontrado.");

                product.SupplierId = dto.SupplierId.Value;
            }

            await _context.SaveChangesAsync();

            return new ApiResponse<Product>
            {
                Message = "Produto atualizado com sucesso.",
                Data = product
            };
        }

        public async Task<ApiResponse<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
                throw new InvalidOperationException("Produto não encontrado.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new ApiResponse<Product>
            {
                Message = "Produto removido com sucesso.",
                Data = product
            };
        }
    }
}
