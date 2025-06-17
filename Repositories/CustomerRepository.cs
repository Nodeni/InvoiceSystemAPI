using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        // Fetch all customers from the database
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        // Fetch a specific customer by ID
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        // Save a new customer to the database
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
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
