namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceResponseDTO
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VAT { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public string UserOrganization { get; set; }
        public string UserEmail { get; set; }
    }

}
