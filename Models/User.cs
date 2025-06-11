using System.ComponentModel.DataAnnotations;

namespace InvoiceSystemAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string OrganizationName {  get; set; }
        public string OrganizationNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? Bankgiro { get; set; }
        public string? IBAN { get; set; }
        public string? SwishNumber { get; set; }

        public DateTime UserCreatedDate { get; set; } = DateTime.UtcNow;

    }
}
