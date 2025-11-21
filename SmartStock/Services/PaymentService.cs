using Microsoft.EntityFrameworkCore;
using SmartStock.Data;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Enums;
using SmartStock.Responses;

namespace SmartStock.Services
{
    public class PaymentService
    {
        private readonly DataContext _context;

        public PaymentService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<Payment>>> FindAll(PaymentFiltersDto? filters)
        {
            var page = filters?.Page ?? 1;
            var pageSize = filters?.PageSize ?? 20;

            var query = _context.Payments
                .AsNoTracking()
                .OrderBy(p => p.CreatedAt);

            var payments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ApiResponse<List<Payment>>
            {
                Message = "Pagamentos listados com sucesso.",
                Data = payments
            };
        }

        public async Task<ApiResponse<Payment>> FindOne(int id)
        {
            var payment = await _context.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment is null)
                throw new InvalidOperationException("Pagamento não encontrado.");

            return new ApiResponse<Payment>
            {
                Message = "Pagamento encontrado com sucesso.",
                Data = payment
            };
        }

        public async Task<ApiResponse<Payment>> CreatePayment(CreatePaymentDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Payload não pode ser nulo.");

            var order = await _context.Orders.FindAsync(dto.OrderId);

            if (order is null)
                throw new InvalidOperationException("Pedido não encontrado.");

            if (order.Status == OrderStatus.CANCELLED)
                throw new InvalidOperationException("Não é possível pagar um pedido cancelado.");

            if (order.Status == OrderStatus.PAID)
                throw new InvalidOperationException("O pedido já está pago.");

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                Method = dto.Method,
            };

            order.Status = OrderStatus.PAID;

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new ApiResponse<Payment>
            {
                Message = "Pagamento criado com sucesso.",
                Data = payment
            };
        }
    }
}
