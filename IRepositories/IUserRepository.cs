using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IRepositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
    }
}
