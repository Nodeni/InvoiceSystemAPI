using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
    }
}
