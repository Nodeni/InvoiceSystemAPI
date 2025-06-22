using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IServiceItemRepository
    {
        Task<ServiceItem> CreateServiceItemAsync(ServiceItem item);
        Task<IEnumerable<ServiceItem>> GetServiceItemsByUserAsync(int userId);
        Task<bool> DeleteServiceItemAsync(int id);
        Task<ServiceItem?> UpdateServiceItemAsync(int id, ServiceItem item);
    }
}
