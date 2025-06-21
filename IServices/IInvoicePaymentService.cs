using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IServices
{
    public interface IInvoicePaymentService
    {
        Task<InvoicePayment> AddPaymentAsync(InvoicePaymentCreateDTO dto);
        Task<IEnumerable<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId);
    }
}
