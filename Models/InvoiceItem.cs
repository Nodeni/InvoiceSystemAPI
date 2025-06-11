using System.ComponentModel.DataAnnotations;

namespace InvoiceSystemAPI.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total {  get; set; }
    }
}
