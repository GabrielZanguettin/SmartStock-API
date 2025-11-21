using System.ComponentModel.DataAnnotations;
using SmartStock.Enums;

namespace SmartStock.Dtos
{
    /// <summary>Data transfer object used to create a new stock movement.</summary>
    public class CreateStockMovementDto
    {
        /// <summary>Identifier of the product affected by this stock movement.</summary>
        /// <example>1</example>
        [Required(ErrorMessage = "O ProductId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ProductId deve ser um inteiro positivo.")]
        public int ProductId { get; set; }

        /// <summary>
        /// Quantity of items that will change in stock 
        /// </summary>
        /// <example>10</example>
        [Required(ErrorMessage = "A QuantityChange é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A QuantityChange deve ser maior que zero.")]
        public int QuantityChange { get; set; }

        /// <summary>Type of stock movement (ENTRY or EXIT).</summary>
        /// <example>ENTRY</example>
        [Required(ErrorMessage = "O Type é obrigatório.")]
        [EnumDataType(typeof(StockMovementType), ErrorMessage = "O Type informado é inválido.")]
        public StockMovementType Type { get; set; }

        /// <summary>Optional identifier of the order related to this stock movement.</summary>
        /// <example>3</example>
        [Range(1, int.MaxValue, ErrorMessage = "O OrderId deve ser um inteiro positivo.")]
        public int? OrderId { get; set; }
    }
}