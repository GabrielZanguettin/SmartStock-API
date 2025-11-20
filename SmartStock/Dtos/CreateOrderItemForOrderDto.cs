using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>Data transfer object used to create an item as part of a new order.</summary>
    public class CreateOrderItemForOrderDto
    {
        /// <summary>Identifier of the product associated with the order item.</summary>
        /// <example>1</example>
        [Range(1, int.MaxValue, ErrorMessage = "O ProductId deve ser um inteiro positivo.")]
        public int ProductId { get; set; }

        /// <summary>Quantity of the product in the order.</summary>
        /// <example>2</example>
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }
    }
}