using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>Data transfer object used to create a new product.</summary>
    public class CreateProductDto
    {
        /// <summary>Name of the product.</summary>
        /// <example>Tênis esportivo básico</example>
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = null!;

        /// <summary>Unit price of the product.</summary>
        /// <example>199.90</example>
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal UnitPrice { get; set; }

        /// <summary>Initial stock quantity of the product.</summary>
        /// <example>50</example>
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa.")]
        public int StockQuantity { get; set; }

        /// <summary>Identifier of the supplier associated with the product.</summary>
        /// <example>1</example>
        [Range(1, int.MaxValue, ErrorMessage = "O SupplierId deve ser um inteiro positivo.")]
        public int SupplierId { get; set; }
    }
}
