using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>Data transfer object used to create a new order.</summary>
    public class CreateOrderDto
    {
        /// <summary>Name of the customer related to the order.</summary>
        /// <example>Maria da Silva</example>
        [StringLength(100, ErrorMessage = "O nome do cliente deve ter no máximo 100 caracteres.")]
        public string? CustomerName { get; set; }

        /// <summary>Discount applied to the order.</summary>
        /// <example>10.00</example>
        [Range(0, double.MaxValue, ErrorMessage = "O desconto não pode ser negativo.")]
        public decimal Discount { get; set; } = 0;

        /// <summary>Taxes applied to the order.</summary>
        /// <example>5.50</example>
        [Range(0, double.MaxValue, ErrorMessage = "Os impostos não podem ser negativos.")]
        public decimal Taxes { get; set; } = 0;

        /// <summary>List of items that compose the order.</summary>
        /// <example>[{ "productId": 1, "quantity": 2 }]</example>
        [Required(ErrorMessage = "O pedido deve conter pelo menos um item.")]
        [MinLength(1, ErrorMessage = "O pedido deve conter pelo menos um item.")]
        public List<CreateOrderItemForOrderDto> Items { get; set; } = new();
    }
}