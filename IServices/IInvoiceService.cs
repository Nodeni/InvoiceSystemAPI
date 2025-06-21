using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IServices
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseDTO> CreateInvoiceAsync(InvoiceCreateDTO dto);
    }
}
