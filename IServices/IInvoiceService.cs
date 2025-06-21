using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IServices
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseDTO> CreateInvoiceAsync(InvoiceCreateDTO dto);
        Task<InvoiceResponseDTO> CreateInvoiceWithResponseAsync(InvoiceCreateDTO dto);
        Task<IEnumerable<InvoiceListDTO>> GetAllInvoicesAsync();
        Task<IEnumerable<Invoice>> GetAllInvoicesByUserAsync(int userId);
        Task<IEnumerable<InvoiceListDTO>> GetInvoiceListByUserIdAsync(int userId);
        Task<InvoiceDetailsDTO?> GetInvoiceDetailsByIdAsync(int id);
        Task<bool> UpdateInvoiceAsync(int id, InvoiceUpdateDTO dto);
    }
}
