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
            return await _serviceItemRepository.CreateServiceItemAsync(dto);
        }

        // Get all service items for a specific user
        public async Task<IEnumerable<ServiceItemListDTO>> GetServiceItemsByUserAsync(int userId)
        {
            return await _serviceItemRepository.GetServiceItemsByUserAsync(userId);
        }

        // Delete a specific service item
        public async Task<bool> DeleteServiceItemAsync(int id)
        {
            return await _serviceItemRepository.DeleteServiceItemAsync(id);
        }
    }
}
