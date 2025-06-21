using InvoiceSystemAPI.DTOs;

namespace InvoiceSystemAPI.IServices
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseDTO> CreateInvoiceAsync(InvoiceCreateDTO dto);
        Task<InvoiceResponseDTO> CreateInvoiceWithResponseAsync(InvoiceCreateDTO dto);
        Task<IEnumerable<InvoiceListDTO>> GetAllInvoicesAsync();
        Task<IEnumerable<InvoiceListDTO>> GetInvoiceListByUserIdAsync(int userId);
        Task<InvoiceDetailsDTO?> GetInvoiceDetailsByIdAsync(int id);
        Task<bool> UpdateInvoiceAsync(int id, InvoiceUpdateDTO dto);
        Task<bool> DeleteInvoiceAsync(int id);
        Task<InvoiceWithPaymentsDTO?> GetInvoiceWithPaymentsAsync(int invoiceId);
    }
}
