using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Data;
using Microsoft.EntityFrameworkCore;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;

namespace InvoiceSystemAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public UserRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // Get all users from the database
        public async Task<List<UserListDTO>> GetAllUsersAsync()
        {
            return await _userService.GetAllUsersAsync();
        }

        // Save a new user to the database
        public async Task<User> CreateUserAsync(UserCreateDTO dto)
        {
            return await _userService.CreateUserAsync(dto);
        }

        // Get a specific user by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }
    }
}
