using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.DTOs;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IInvoicePaymentRepository
    {
        Task<InvoicePayment> AddPaymentAsync(InvoicePaymentCreateDTO dto);
        Task<IEnumerable<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId);
    }
}
