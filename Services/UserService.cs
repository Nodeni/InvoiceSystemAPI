using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Return a list of users with selected info
        public async Task<List<UserListDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserListDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    OrganizationName = u.OrganizationName
                })
                .ToListAsync();
        }

        // Create a new user and save to DB
        public async Task<User> CreateUserAsync(UserCreateDTO dto)
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

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // Get a user by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
