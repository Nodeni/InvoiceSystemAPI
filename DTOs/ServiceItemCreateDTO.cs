namespace InvoiceSystemAPI.DTOs
{
    public class ServiceItemCreateDTO
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
