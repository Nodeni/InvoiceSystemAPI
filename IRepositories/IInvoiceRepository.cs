using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateInvoiceAsync(InvoiceCreateDTO dto);
    }
}
