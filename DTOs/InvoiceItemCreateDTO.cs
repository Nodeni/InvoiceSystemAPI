namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceItemCreateDTO
    {
        public int InvoiceId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}
