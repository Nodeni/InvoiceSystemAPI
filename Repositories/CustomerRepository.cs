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

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<List<Customer>> GetCustomersByUserIdAsync(int userId)
        {
            return await _context.Customers
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

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
    }
}
