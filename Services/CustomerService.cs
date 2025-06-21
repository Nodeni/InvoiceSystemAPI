using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Creates a customer from DTO and returns list-style DTO
        public async Task<CustomerListDTO> CreateCustomerAsync(CustomerCreateDTO dto)
        {
            var customer = new Customer
            {
                UserId = dto.UserId,
                IsCompany = dto.IsCompany,
                CompanyName = dto.CompanyName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                AddressLine1 = dto.AddressLine1,
                ZipCode = dto.ZipCode,
                City = dto.City,
                Country = dto.Country,
                OrganizationNumber = dto.OrganizationNumber,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _customerRepository.CreateCustomerAsync(customer);

            return new CustomerListDTO
            {
                Id = created.Id,
                IsCompany = created.IsCompany,
                CompanyName = created.CompanyName,
                FirstName = created.FirstName,
                LastName = created.LastName,
                Email = created.Email,
                City = created.City,
                Country = created.Country
            };
        }

        // Get all customers and return them as list DTOs
        public async Task<IEnumerable<CustomerListDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            return customers.Select(c => new CustomerListDTO
            {
                Id = c.Id,
                IsCompany = c.IsCompany,
                CompanyName = c.CompanyName,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                City = c.City,
                Country = c.Country
            });
        }

        // Get a single customer by ID and return list-style DTO
        public async Task<CustomerListDTO?> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return null;

            return new CustomerListDTO
            {
                Id = customer.Id,
                IsCompany = customer.IsCompany,
                CompanyName = customer.CompanyName,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                City = customer.City,
                Country = customer.Country
            };
        }

        // Get all customers belonging to a specific user
        public async Task<IEnumerable<CustomerListDTO>> GetCustomersByUserIdAsync(int userId)
        {
            var customers = await _customerRepository.GetCustomersByUserIdAsync(userId);

            return customers.Select(c => new CustomerListDTO
            {
                Id = c.Id,
                IsCompany = c.IsCompany,
                CompanyName = c.CompanyName,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                City = c.City,
                Country = c.Country
            });
        }

        // Update customer info from DTO
        public async Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDTO dto)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return false;

            customer.IsCompany = dto.IsCompany;
            customer.CompanyName = dto.CompanyName;
            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.AddressLine1 = dto.AddressLine1;
            customer.ZipCode = dto.ZipCode;
            customer.City = dto.City;
            customer.Country = dto.Country;
            customer.OrganizationNumber = dto.OrganizationNumber;

            return await _customerRepository.UpdateCustomerAsync(customer);
        }

        // Delete customer by ID
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
