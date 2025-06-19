namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceDetailsDTO
    {
        // Invoiceinfo
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VAT { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }

        // Customerinfo
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerOrgNr { get; set; }

        // Organizationinfo
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyZipCode { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyCountry { get; set; }
        public string Bankgiro { get; set; }
        public string IBAN { get; set; }
        public string SwishNumber { get; set; }

        // Invoice item details
        public List<InvoiceItemDetailsDTO> Items { get; set; }
    }
}
