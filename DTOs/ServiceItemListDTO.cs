namespace InvoiceSystemAPI.DTOs
{
    public class ServiceItemListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}