using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(CustomerCreateDTO dto);
        Task<List<Customer>> GetCustomersByUserIdAsync(int userId);
        Task<Customer?> UpdateCustomerAsync(int id, CustomerUpdateDTO dto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
