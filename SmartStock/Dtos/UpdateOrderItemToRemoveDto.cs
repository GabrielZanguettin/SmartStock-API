using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>Represents an item to be removed from the order.</summary>
    public class UpdateOrderItemToRemoveDto
    {
        /// <summary>Identifier of the order item to be removed.</summary>
        /// <example>10</example>
        [Required(ErrorMessage = "O OrderItemId é obrigatório para remover um item.")]
        [Range(1, int.MaxValue, ErrorMessage = "O OrderItemId deve ser um inteiro positivo.")]
        public int OrderItemId { get; set; }
    }
}