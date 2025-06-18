namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceItemDetailsDTO
    {
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
