using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SmartStock.Dtos
{
    /// <summary>Represents an item to be edited in the order.</summary>
    public class UpdateOrderItemToEditDto : IValidatableObject
    {
        /// <summary>Identifier of the order item to be edited.</summary>
        /// <example>10</example>
        [Required(ErrorMessage = "O OrderItemId é obrigatório para editar um item.")]
        [Range(1, int.MaxValue, ErrorMessage = "O OrderItemId deve ser um inteiro positivo.")]
        public int OrderItemId { get; set; }

        /// <summary>New product identifier for the order item (optional).</summary>
        /// <example>2</example>
        [Range(1, int.MaxValue, ErrorMessage = "O ProductId deve ser um inteiro positivo.")]
        public int? ProductId { get; set; }

        /// <summary>New quantity for the order item (optional).</summary>
        /// <example>3</example>
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int? Quantity { get; set; }

        /// <summary>
        /// Ensure that at least ProductId or Quantity is provided.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ProductId.HasValue && !Quantity.HasValue)
            {
                yield return new ValidationResult(
                    "É necessário informar pelo menos ProductId ou Quantity para editar um item.",
                    new[] { nameof(ProductId), nameof(Quantity) }
                );
            }
        }
    }
}