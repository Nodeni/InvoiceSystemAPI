namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceItemCreateDTO
    {
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
