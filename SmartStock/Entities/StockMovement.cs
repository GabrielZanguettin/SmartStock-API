using SmartStock.Enums;

namespace SmartStock.Entities
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int QuantityChange { get; set; }
        public StockMovementType Type { get; set; }
        public int? OrderId { get; set; }
    }
}
