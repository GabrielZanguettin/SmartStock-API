using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>Data transfer object used to create a new supplier.</summary>
    public class CreateSupplierDto
    {
        /// <summary>Name of the supplier.</summary>
        /// <example>Fornecedor Esportivo LTDA</example>
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public string Name { get; set; } = null!;

        /// <summary>Tax identification document of the supplier (CPF/CNPJ or equivalent).</summary>
        /// <example>12.345.678/0001-99</example>
        [StringLength(20, ErrorMessage = "O documento deve ter no máximo 20 caracteres.")]
        public string? Document { get; set; }

        /// <summary>Contact phone number of the supplier.</summary>
        /// <example>(11) 98765-4321</example>
        [Phone(ErrorMessage = "O telefone informado não é válido.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string? Phone { get; set; }

        /// <summary>Contact email address of the supplier.</summary>
        /// <example>contato@fornecedor.com</example>
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string? Email { get; set; }
    }
}
