using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserController(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        // Get all users from the database
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        // Create a new user
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDTO dto)
        {
            var user = await _userRepository.CreateUserAsync(dto);

            var response = new UserResponseDTO
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                OrganizationName = user.OrganizationName,
                CreatedAt = user.UserCreatedDate
            };

            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, response);
        }


        // Get a specific user by their ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
