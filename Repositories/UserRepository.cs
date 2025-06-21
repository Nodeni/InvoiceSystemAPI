using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // Save a new user to the database
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Get all users from the database
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Get a user by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
