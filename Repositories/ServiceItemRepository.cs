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
        public async Task<ServiceItem> CreateServiceItemAsync(ServiceItemCreateDTO dto)
        {
            var serviceItem = new ServiceItem
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice
            };

            _context.ServiceItems.Add(serviceItem);
            await _context.SaveChangesAsync();

            return serviceItem;
        }

        // Get all services for a user
        public async Task<IEnumerable<ServiceItemListDTO>> GetServiceItemsByUserAsync(int userId)
        {
            return await _context.ServiceItems
                .Where(s => s.UserId == userId)
                .Select(s => new ServiceItemListDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    UnitPrice = s.UnitPrice
                })
                .ToListAsync();
        }

        // Delete a service item
        public async Task<bool> DeleteServiceItemAsync(int id)
        {
            var item = await _context.ServiceItems.FindAsync(id);
            if (item == null)
                return false;

            _context.ServiceItems.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
