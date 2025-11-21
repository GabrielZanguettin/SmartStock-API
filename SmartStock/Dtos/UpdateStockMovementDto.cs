using SmartStock.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>
    /// Data transfer object used to partially update an existing stock movement.
    /// All properties are optional; only provided values will be updated.
    /// </summary>
    public class UpdateStockMovementDto
    {
        /// <summary>Identifier of the product affected by this stock movement.</summary>
        /// <example>2</example>
        [Range(1, int.MaxValue, ErrorMessage = "O ProductId deve ser um inteiro positivo.")]
        public int? ProductId { get; set; }

        /// <summary>
        /// Quantity of items that will change in stock 
        /// </summary>
        /// <example>15</example>
        [Range(1, int.MaxValue, ErrorMessage = "A QuantityChange deve ser maior que zero.")]
        public int? QuantityChange { get; set; }

        /// <summary>Type of stock movement (ENTRY or EXIT).</summary>
        /// <example>EXIT</example>
        [EnumDataType(typeof(StockMovementType), ErrorMessage = "O Type informado é inválido.")]
        public StockMovementType? Type { get; set; }

        /// <summary>Optional identifier of the order related to this stock movement.</summary>
        /// <example>5</example>
        [Range(1, int.MaxValue, ErrorMessage = "O OrderId deve ser um inteiro positivo.")]
        public int? OrderId { get; set; }
    }
}
