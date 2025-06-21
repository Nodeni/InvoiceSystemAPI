using Microsoft.AspNetCore.Mvc;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDTO dto)
        {
            var user = await _userService.CreateUserAsync(dto);

            var response = new UserResponseDTO
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                OrganizationName = user.OrganizationName,
                CreatedAt = DateTime.UtcNow
            };

            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
