namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceCreateDTO
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public DateTime DueDate { get; set; }
        public List<InvoiceItemCreateDTO> Items { get; set; }
    }
}
