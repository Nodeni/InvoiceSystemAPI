using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceWithPaymentsDTO
    {
        public InvoiceDetailsDTO Invoice { get; set; }
        public IEnumerable<InvoicePayment> Payments { get; set; }
    }
}
