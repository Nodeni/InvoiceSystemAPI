using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IInvoicePaymentRepository
    {
        Task<InvoicePayment> AddPaymentAsync(InvoicePayment payment);
        Task<IEnumerable<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId);
        Task<decimal> GetTotalPaidAmountAsync(int invoiceId);
        Task UpdateInvoiceStatusAsync(int invoiceId, bool isPaid);
    }
}
