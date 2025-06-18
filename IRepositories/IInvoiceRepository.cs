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

    }
}
