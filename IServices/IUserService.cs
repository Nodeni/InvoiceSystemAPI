using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.IServices
{
    public interface IUserService
    {
        Task<List<UserListDTO>> GetAllUsersAsync();
        Task<User> CreateUserAsync(UserCreateDTO dto);
        Task<User?> GetUserByIdAsync(int id);
    }
}
