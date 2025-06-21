using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<IEnumerable<Invoice>> GetAllInvoicesByUserAsync(int userId);
        Task<Invoice?> GetInvoiceByIdAsync(int id);
        Task<bool> UpdateInvoiceAsync(Invoice invoice);
        Task<bool> DeleteInvoiceAsync(int id);
        Task<Invoice?> GetInvoiceWithCustomerAndUserAsync(int id);
        Task<List<InvoiceItem>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId);
        Task<List<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId);
    }
}
