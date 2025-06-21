using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.Services
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly IServiceItemRepository _serviceItemRepository;

        public ServiceItemService(IServiceItemRepository serviceItemRepository)
        {
            _serviceItemRepository = serviceItemRepository;
        }

        // Create a new service item
        public async Task<ServiceItem> CreateServiceItemAsync(ServiceItemCreateDTO dto)
        {
            var item = new ServiceItem
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice
            };
            return await _serviceItemRepository.CreateServiceItemAsync(item);
        }

        // Get all service items for a specific user
        public async Task<IEnumerable<ServiceItemListDTO>> GetServiceItemsByUserAsync(int userId)
        {
            var services = await _serviceItemRepository.GetServiceItemsByUserAsync(userId);

            return services.Select(s => new ServiceItemListDTO
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                UnitPrice = s.UnitPrice
            });
        }

        // Delete a specific service item
        public async Task<bool> DeleteServiceItemAsync(int id)
        {
            return await _serviceItemRepository.DeleteServiceItemAsync(id);
        }

        //Update a specific service item
        public async Task<ServiceItem?> UpdateServiceItemAsync(int id, ServiceItemUpdateDTO dto)
        {
            var item = new ServiceItem
            {
                Title = dto.Title,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice
            };
            return await _serviceItemRepository.UpdateServiceItemAsync(id, item);
        }
    }
}
