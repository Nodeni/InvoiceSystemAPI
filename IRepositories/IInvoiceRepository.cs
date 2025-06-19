using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateInvoiceAsync(InvoiceCreateDTO dto);
        Task<IEnumerable<Invoice>> GetAllInvoicesByUserAsync(int userId);
        Task<InvoiceDetailsDTO> GetInvoiceDetailsByIdAsync(int id);
        Task<IEnumerable<InvoiceListDTO>> GetAllInvoicesAsync();
        Task<bool> UpdateInvoiceAsync(int id, InvoiceUpdateDTO dto);
        Task<bool> DeleteInvoiceAsync(int id);
        Task<InvoiceWithPaymentsDTO?> GetInvoiceWithPaymentsAsync(int invoiceId);
    }
}
