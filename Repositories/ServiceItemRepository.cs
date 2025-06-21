using InvoiceSystemAPI.Data;
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

        // Create a new service item
        public async Task<ServiceItem> CreateServiceItemAsync(ServiceItem serviceItem)
        {
            _context.ServiceItems.Add(serviceItem);
            await _context.SaveChangesAsync();
            return serviceItem;
        }

        // Get all service items for a user
        public async Task<IEnumerable<ServiceItem>> GetServiceItemsByUserAsync(int userId)
        {
            return await _context.ServiceItems
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        // Update a service item
        public async Task<ServiceItem?> UpdateServiceItemAsync(int id, ServiceItem serviceItem)
        {
            var existing = await _context.ServiceItems.FindAsync(id);
            if (existing == null) return null;

            existing.Title = serviceItem.Title;
            existing.Description = serviceItem.Description;
            existing.UnitPrice = serviceItem.UnitPrice;

            await _context.SaveChangesAsync();
            return existing;
        }

        // Delete a service item
        public async Task<bool> DeleteServiceItemAsync(int id)
        {
            var item = await _context.ServiceItems.FindAsync(id);
            if (item == null) return false;

            _context.ServiceItems.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
