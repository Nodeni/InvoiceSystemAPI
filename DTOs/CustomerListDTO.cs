namespace InvoiceSystemAPI.DTOs
{
    public class CustomerListDTO
    {
        public int Id { get; set; }
        public bool IsCompany { get; set; }
        public string? CompanyName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
