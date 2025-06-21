using InvoiceSystemAPI.DTOs;

namespace InvoiceSystemAPI.IServices
{
    public interface IUserService
    {
        Task<UserListDTO> CreateUserAsync(UserCreateDTO dto);
        Task<IEnumerable<UserListDTO>> GetAllUsersAsync();
        Task<UserListDTO?> GetUserByIdAsync(int id);
    }
}
