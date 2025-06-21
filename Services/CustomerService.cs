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
    }
}
