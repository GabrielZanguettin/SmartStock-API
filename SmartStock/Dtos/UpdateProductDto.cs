using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>
    /// Data transfer object used to partially update an existing product.
    /// All properties are optional; only provided values will be updated.
    /// </summary>
    public class UpdateProductDto
    {
        /// <summary>New name for the product (optional).</summary>
        /// <example>Tênis esportivo premium</example>
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres.")]
        public string? Name { get; set; }

        /// <summary>New unit price for the product (optional).</summary>
        /// <example>219.90</example>
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal? UnitPrice { get; set; }

        /// <summary>New supplier identifier for the product (optional).</summary>
        /// <example>2</example>
        [Range(1, int.MaxValue, ErrorMessage = "O SupplierId deve ser um inteiro positivo.")]
        public int? SupplierId { get; set; }
    }
}
