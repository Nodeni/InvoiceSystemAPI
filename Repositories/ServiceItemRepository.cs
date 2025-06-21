using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Repositories
{
    public class ServiceItemRepository : IServiceItemRepository
    {
        private readonly AppDbContext _context;

        public ServiceItemRepository(AppDbContext context)
        {
            _context = context;
        }

        // Save a new service item
        public async Task<ServiceItem> CreateServiceItemAsync(ServiceItem item)
        {
            _context.ServiceItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // Get all services for a user
        public async Task<IEnumerable<ServiceItem>> GetServiceItemsByUserAsync(int userId)
        {
            return await _context.ServiceItems
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        // Delete a service item
        public async Task<bool> DeleteServiceItemAsync(int id)
        {
            var item = await _context.ServiceItems.FindAsync(id);
            if (item == null) return false;

            _context.ServiceItems.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }

        // Update an existing service item
        public async Task<ServiceItem?> UpdateServiceItemAsync(int id, ServiceItem updatedItem)
        {
            var item = await _context.ServiceItems.FindAsync(id);
            if (item == null) return null;

            item.Title = updatedItem.Title;
            item.Description = updatedItem.Description;
            item.UnitPrice = updatedItem.UnitPrice;

            await _context.SaveChangesAsync();
            return item;
        }
    }
}
