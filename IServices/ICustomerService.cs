using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerListDTO>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetCustomersByUserIdAsync(int userId);
        Task<Customer?> UpdateCustomerAsync(int id, CustomerUpdateDTO dto);
        Task<bool> DeleteCustomerAsync(int id);
        Task<Customer> CreateCustomerAsync(CustomerCreateDTO dto);
    }
}
