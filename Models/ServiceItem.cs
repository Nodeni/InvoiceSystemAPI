using System.ComponentModel.DataAnnotations;

namespace InvoiceSystemAPI.Models
{
    public class ServiceItem
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string? UnitType { get; set; }
    }
}
