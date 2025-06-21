using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IServices
{
    public interface IInvoiceItemService
    {
        Task<InvoiceItem> CreateInvoiceItemAsync(InvoiceItemCreateDTO dto);
        Task<IEnumerable<InvoiceItemDetailsDTO>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId);
        Task<bool> DeleteInvoiceItemAsync(int id);
    }
}
