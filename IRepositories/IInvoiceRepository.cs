using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<InvoiceResponseDTO> CreateInvoiceAsync(InvoiceCreateDTO dto);
        Task<IEnumerable<Invoice>> GetAllInvoicesByUserAsync(int userId);
        Task<InvoiceDetailsDTO> GetInvoiceDetailsByIdAsync(int id);
        Task<bool> UpdateInvoiceAsync(int id, InvoiceUpdateDTO dto);
        Task<bool> DeleteInvoiceAsync(int id);
        Task<InvoiceWithPaymentsDTO?> GetInvoiceWithPaymentsAsync(int invoiceId);
        Task<IEnumerable<InvoiceListDTO>> GetInvoiceListByUserIdAsync(int userId);
        Task<InvoiceResponseDTO> CreateInvoiceWithResponseAsync(InvoiceCreateDTO dto);
        Task<IEnumerable<InvoiceListDTO>> GetAllInvoicesAsync();
    }
}
