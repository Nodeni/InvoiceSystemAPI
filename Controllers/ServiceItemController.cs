using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceItemController : ControllerBase
    {
        private readonly IServiceItemRepository _serviceItemRepository;

        public ServiceItemController(IServiceItemRepository serviceItemRepository)
        {
            _serviceItemRepository = serviceItemRepository;
        }

        // POST: Create a new service item
        [HttpPost]
        public async Task<IActionResult> CreateServiceItem([FromBody] ServiceItemCreateDTO dto)
        {
            var serviceItem = await _serviceItemRepository.CreateServiceItemAsync(dto);

            return CreatedAtAction(nameof(GetServiceItemsByUser), new { userId = dto.UserId }, serviceItem);
        }

        // GET: Get all service items by user ID
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetServiceItemsByUser(int userId)
        {
            var items = await _serviceItemRepository.GetServiceItemsByUserAsync(userId);

            return Ok(items);
        }

        // DELETE: Delete a specific service item by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceItem(int id)
        {
            var deleted = await _serviceItemRepository.DeleteServiceItemAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
