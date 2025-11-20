using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>Data transfer object used to create a new order item.</summary>
    public class CreateOrderItemDto
    {
        /// <summary>Identifier of the order associated with this item.</summary>
        /// <example>1</example>
        [Required(ErrorMessage = "O OrderId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O OrderId deve ser um inteiro positivo.")]
        public int OrderId { get; set; }

        /// <summary>Identifier of the product associated with this item.</summary>
        /// <example>1</example>
        [Required(ErrorMessage = "O ProductId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ProductId deve ser um inteiro positivo.")]
        public int ProductId { get; set; }

        /// <summary>Quantity of the product included in this order item.</summary>
        /// <example>50</example>
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
        public int Quantity { get; set; }
    }
}