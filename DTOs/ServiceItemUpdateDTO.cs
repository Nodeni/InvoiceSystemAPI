namespace InvoiceSystemAPI.DTOs
{
    public class ServiceItemUpdateDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
