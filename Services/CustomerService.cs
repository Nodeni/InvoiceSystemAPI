using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        // Fetch all customers and return as list DTOs
        public async Task<List<CustomerListDTO>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerListDTO
                {
                    Id = c.Id,
                    IsCompany = c.IsCompany,
                    CompanyName = c.CompanyName,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    City = c.City,
                    Country = c.Country
                })
                .ToListAsync();
        }

        // Retrieves a customer by ID from the database
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
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
