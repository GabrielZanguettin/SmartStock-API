using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>Represents an item to be added to the order.</summary>
    public class UpdateOrderItemToAddDto
    {
        /// <summary>Identifier of the product associated with the order item.</summary>
        /// <example>1</example>
        [Required(ErrorMessage = "O ProductId é obrigatório para adicionar um item.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ProductId deve ser um inteiro positivo.")]
        public int ProductId { get; set; }

        /// <summary>Quantity of the product in the order.</summary>
        /// <example>2</example>
        [Required(ErrorMessage = "A quantidade é obrigatória para adicionar um item.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }
    }
}