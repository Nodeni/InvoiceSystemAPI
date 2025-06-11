using System.ComponentModel.DataAnnotations;

namespace InvoiceSystemAPI.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VAT { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "Unpaid";
    }
}
