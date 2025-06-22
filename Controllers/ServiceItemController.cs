using Microsoft.AspNetCore.Mvc;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceItemController : ControllerBase
    {
        private readonly IServiceItemService _serviceItemService;

        public ServiceItemController(IServiceItemService serviceItemService)
        {
            _serviceItemService = serviceItemService;
        }

        // POST: Create a new service item
        [HttpPost]
        public async Task<IActionResult> CreateServiceItem([FromBody] ServiceItemCreateDTO dto)
        {
            var item = await _serviceItemService.CreateServiceItemAsync(dto);
            return CreatedAtAction(nameof(GetServiceItemsByUser), new { userId = dto.UserId }, item);
        }

        // GET: Get all service items by user ID
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetServiceItemsByUser(int userId)
        {
            var items = await _serviceItemService.GetServiceItemsByUserAsync(userId);
            return Ok(items);
        }

        // PUT: Update a service item
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceItem(int id, [FromBody] ServiceItemUpdateDTO dto)
        {
            var updated = await _serviceItemService.UpdateServiceItemAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: Delete a service item
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceItem(int id)
        {
            var deleted = await _serviceItemService.DeleteServiceItemAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
