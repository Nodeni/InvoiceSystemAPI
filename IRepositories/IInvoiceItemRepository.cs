using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IInvoiceItemRepository
    {
        Task<InvoiceItem> CreateInvoiceItemAsync(InvoiceItemCreateDTO dto);
        Task<IEnumerable<InvoiceItemDetailsDTO>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId);
        Task<bool> DeleteInvoiceItemAsync(int id);
    }
}
