namespace InvoiceSystemAPI.DTOs
{
    public class InvoicePaymentCreateDTO
    {
        public int InvoiceId { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal? AmountPaid { get; set; }
    }
}
