using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IInvoiceItemRepository
    {
        Task<InvoiceItem> CreateInvoiceItemAsync(InvoiceItem item);
        Task<IEnumerable<InvoiceItem>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId);
        Task<InvoiceItem?> UpdateInvoiceItemAsync(int id, InvoiceItem item);
        Task<bool> DeleteInvoiceItemAsync(int id);
    }
}
