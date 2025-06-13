using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.IRepositories;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDTO dto)
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
            await _userRepository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);

        }
    }
}
