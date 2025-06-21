using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using InvoiceSystemAPI.IServices;


namespace InvoiceSystemAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly ICustomerService _customerService;

        public CustomerRepository(AppDbContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        // Fetch all customers from the database
        public async Task<List<CustomerListDTO>> GetAllCustomersAsync()
        {
            return await _customerService.GetAllCustomersAsync();
        }

        // Fetch a specific customer by ID
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerService.GetCustomerByIdAsync(id);
        }

        // Save a new customer to the database
        public async Task<Customer> CreateCustomerAsync(CustomerCreateDTO dto)
        {
            return await _customerService.CreateCustomerAsync(dto);
        }


        // Get all customers connected to a specific user
        public async Task<List<Customer>> GetCustomersByUserIdAsync(int userId)
        {
            return await _customerService.GetCustomersByUserIdAsync(userId);
        }

        // Update existing customer information
        public async Task<Customer?> UpdateCustomerAsync(int id, CustomerUpdateDTO dto)
        {
            return await _customerService.UpdateCustomerAsync(id, dto);
        }

        // Delete a customer by ID
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _customerService.DeleteCustomerAsync(id);
        }
    }
}
