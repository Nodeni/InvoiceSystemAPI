using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.Services
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly IServiceItemRepository _repository;

        public ServiceItemService(IServiceItemRepository repository)
        {
            _repository = repository;
        }

        // Create a new service item
        public async Task<ServiceItemListDTO> CreateServiceItemAsync(ServiceItemCreateDTO dto)
        {
            var serviceItem = new ServiceItem
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice
            };

            var created = await _repository.CreateServiceItemAsync(serviceItem);

            return new ServiceItemListDTO
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                UnitPrice = created.UnitPrice
            };
        }

        // Get all service items for a user
        public async Task<IEnumerable<ServiceItemListDTO>> GetServiceItemsByUserAsync(int userId)
        {
            var items = await _repository.GetServiceItemsByUserAsync(userId);

            return items.Select(s => new ServiceItemListDTO
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                UnitPrice = s.UnitPrice
            });
        }

        // Update a service item
        public async Task<ServiceItemListDTO?> UpdateServiceItemAsync(int id, ServiceItemUpdateDTO dto)
        {
            var serviceItem = new ServiceItem
            {
                Title = dto.Title,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice
            };

            var updated = await _repository.UpdateServiceItemAsync(id, serviceItem);
            if (updated == null) return null;

            return new ServiceItemListDTO
            {
                Id = updated.Id,
                Title = updated.Title,
                Description = updated.Description,
                UnitPrice = updated.UnitPrice
            };
        }

        // Delete a service item
        public async Task<bool> DeleteServiceItemAsync(int id)
        {
            return await _repository.DeleteServiceItemAsync(id);
        }
    }
}
