using SmartStock.Enums;

namespace SmartStock.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CustomerName { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.DRAFT;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal Subtotal => Items.Sum(item => item.Quantity * item.UnitPrice);
        public decimal Taxes { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => Subtotal + Taxes - Discount;
    }
}
