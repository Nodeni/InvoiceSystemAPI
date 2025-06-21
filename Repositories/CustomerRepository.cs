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
            return await _context.Customers.FindAsync(id);
        }

        // Save a new customer to the database
        public async Task<Customer> CreateCustomerAsync(CustomerCreateDTO dto)
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

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }


        // Get all customers connected to a specific user
        public async Task<List<Customer>> GetCustomersByUserIdAsync(int userId)
        {
            return await _context.Customers
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        // Update existing customer information
        public async Task<Customer?> UpdateCustomerAsync(int id, CustomerUpdateDTO dto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return null;

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

            await _context.SaveChangesAsync();
            return customer;
        }

        // Delete a customer by ID
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
