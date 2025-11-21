using System.ComponentModel.DataAnnotations;

namespace SmartStock.Dtos
{
    /// <summary>Data transfer object used to update an existing order.</summary>
    public class UpdateOrderDto
    {
        /// <summary>Name of the customer related to the order.</summary>
        /// <example>Maria da Silva</example>
        [StringLength(100, ErrorMessage = "O nome do cliente deve ter no máximo 100 caracteres.")]
        public string? CustomerName { get; set; }

        /// <summary>Discount applied to the order.</summary>
        /// <example>10.00</example>
        [Range(0, double.MaxValue, ErrorMessage = "O desconto não pode ser negativo.")]
        public decimal? Discount { get; set; }

        /// <summary>Taxes applied to the order.</summary>
        /// <example>5.50</example>
        [Range(0, double.MaxValue, ErrorMessage = "Os impostos não podem ser negativos.")]
        public decimal? Taxes { get; set; }

        /// <summary>Items to be added to the order.</summary>
        /// <example>[{ "productId": 1, "quantity": 2 }]</example>
        public List<UpdateOrderItemToAddDto>? ToAdd { get; set; }

        /// <summary>Items to be removed from the order.</summary>
        /// <example>[{ "orderItemId": 10 }]</example>
        public List<UpdateOrderItemToRemoveDto>? ToRemove { get; set; }

        /// <summary>Items to be edited in the order.</summary>
        /// <example>[{ "orderItemId": 10, "productId": 2, "quantity": 3 }]</example>
        public List<UpdateOrderItemToEditDto>? ToEdit { get; set; }
    }
}