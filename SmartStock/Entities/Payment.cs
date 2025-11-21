using SmartStock.Enums;

namespace SmartStock.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.PAID;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
