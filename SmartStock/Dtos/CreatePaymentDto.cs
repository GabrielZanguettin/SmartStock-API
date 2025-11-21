using System.ComponentModel.DataAnnotations;
using SmartStock.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartStock.Dtos
{
    /// <summary>
    /// Data transfer object used to create a payment for an existing order.
    /// </summary>
    public class CreatePaymentDto
    {
        /// <summary>
        /// Identifier of the order that will be paid.
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "O OrderId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O OrderId deve ser um inteiro positivo.")]
        public int OrderId { get; set; }

        /// <summary>
        /// Payment method used to pay the order.
        /// </summary>
        /// <example>PIX</example>
        [Required(ErrorMessage = "O método é obrigatório.")]
        [EnumDataType(typeof(PaymentMethod), ErrorMessage = "O método de pagamento informado é inválido.")]
        public PaymentMethod Method { get; set; }
    }
}