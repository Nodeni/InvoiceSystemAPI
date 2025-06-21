using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IServices
{
    public interface IServiceItemService
    {
        Task<ServiceItem> CreateServiceItemAsync(ServiceItemCreateDTO dto);
        Task<IEnumerable<ServiceItemListDTO>> GetServiceItemsByUserAsync(int userId);
        Task<bool> DeleteServiceItemAsync(int id);
        Task<ServiceItem?> UpdateServiceItemAsync(int id, ServiceItemUpdateDTO dto);
    }
}
