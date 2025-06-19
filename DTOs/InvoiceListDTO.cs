namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceListDTO
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
    }
}
