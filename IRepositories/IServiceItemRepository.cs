using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IServiceItemRepository
    {
        Task<ServiceItem> CreateServiceItemAsync(ServiceItemCreateDTO dto);
        Task<IEnumerable<ServiceItemListDTO>> GetServiceItemsByUserAsync(int userId);
        Task<bool> DeleteServiceItemAsync(int id);
    }
}
