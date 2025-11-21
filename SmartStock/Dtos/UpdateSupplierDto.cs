using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>
    /// Data transfer object used to partially update an existing supplier.
    /// All properties are optional; only provided values will be updated.
    /// </summary>
    public class UpdateSupplierDto
    {
        /// <summary>Name of the supplier.</summary>
        /// <example>Fornecedor Atualizado ME</example>
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public string? Name { get; set; }

        /// <summary>Tax identification document of the supplier (CPF/CNPJ or equivalent).</summary>
        /// <example>98.765.432/0001-55</example>
        [StringLength(20, ErrorMessage = "O documento deve ter no máximo 20 caracteres.")]
        public string? Document { get; set; }

        /// <summary>Contact phone number of the supplier.</summary>
        /// <example>(21) 91234-5678</example>
        [Phone(ErrorMessage = "O telefone informado não é válido.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string? Phone { get; set; }

        /// <summary>Contact email address of the supplier.</summary>
        /// <example>financeiro@fornecedoratualizado.com</example>
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string? Email { get; set; }
    }
}
