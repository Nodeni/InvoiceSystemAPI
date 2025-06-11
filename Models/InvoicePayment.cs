using System.ComponentModel.DataAnnotations;

namespace InvoiceSystemAPI.Models
{
    public class InvoicePayment
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal? AmountPaid { get; set; }
        public bool IsPaid { get; set; }
    }
}
