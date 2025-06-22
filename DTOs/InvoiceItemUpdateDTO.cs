namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceItemUpdateDTO
    {
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
