namespace InvoiceSystemAPI.DTOs
{
    public class CustomerCreateDTO
    {
        public bool IsCompany { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string OrganizationNumber { get; set; }
        public int UserId { get; set; }
    }
}
