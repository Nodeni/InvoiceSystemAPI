using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IServices
{
    public interface IInvoiceItemService
    {
        Task<InvoiceItemDetailsDTO> CreateInvoiceItemAsync(InvoiceItemCreateDTO dto);
        Task<IEnumerable<InvoiceItemDetailsDTO>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId);
        Task<InvoiceItemDetailsDTO?> UpdateInvoiceItemAsync(int id, InvoiceItemUpdateDTO dto);
        Task<bool> DeleteInvoiceItemAsync(int id);
    }
}
