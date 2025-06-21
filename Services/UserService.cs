using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Create a new user and return list-style DTO
        public async Task<UserListDTO> CreateUserAsync(UserCreateDTO dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                OrganizationName = dto.OrganizationName,
                OrganizationNumber = dto.OrganizationNumber,
                AddressLine1 = dto.AddressLine1,
                ZipCode = dto.ZipCode,
                City = dto.City,
                Country = dto.Country,
                Bankgiro = dto.Bankgiro,
                IBAN = dto.IBAN,
                SwishNumber = dto.SwishNumber,
                UserCreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            return new UserListDTO
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email,
                OrganizationName = createdUser.OrganizationName
            };
        }

        // Get all users and return as DTOs
        public async Task<IEnumerable<UserListDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(u => new UserListDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                OrganizationName = u.OrganizationName
            });
        }

        // Get a user by ID and return as DTO
        public async Task<UserListDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return new UserListDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                OrganizationName = user.OrganizationName
            };
        }
    }
}
