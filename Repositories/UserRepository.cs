using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all users from the database
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Save a new user to the database
        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Get a specific user by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
