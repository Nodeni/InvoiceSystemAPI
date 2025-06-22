using InvoiceSystemAPI.DTOs;

namespace InvoiceSystemAPI.IServices
{
    public interface IServiceItemService
    {
        Task<ServiceItemListDTO> CreateServiceItemAsync(ServiceItemCreateDTO dto);
        Task<IEnumerable<ServiceItemListDTO>> GetServiceItemsByUserAsync(int userId);
        Task<ServiceItemListDTO?> UpdateServiceItemAsync(int id, ServiceItemUpdateDTO dto);
        Task<bool> DeleteServiceItemAsync(int id);
    }
}
