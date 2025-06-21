using InvoiceSystemAPI.DTOs;

namespace InvoiceSystemAPI.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerListDTO>> GetAllCustomersAsync();
    }
}
