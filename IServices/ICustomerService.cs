using InvoiceSystemAPI.DTOs;

namespace InvoiceSystemAPI.IServices
{
    public interface ICustomerService
    {
        Task<CustomerListDTO> CreateCustomerAsync(CustomerCreateDTO dto);
        Task<IEnumerable<CustomerListDTO>> GetAllCustomersAsync();
        Task<CustomerListDTO?> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerListDTO>> GetCustomersByUserIdAsync(int userId);
        Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDTO dto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
