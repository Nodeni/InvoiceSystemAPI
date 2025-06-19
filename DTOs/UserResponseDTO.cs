namespace InvoiceSystemAPI.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
