using Microsoft.AspNetCore.Mvc;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceItemController : ControllerBase
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;

        public InvoiceItemController(IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;
        }

        // Create a new invoice item
        [HttpPost]
        public async Task<IActionResult> CreateInvoiceItem([FromBody] InvoiceItemCreateDTO dto)
        {
            var item = await _invoiceItemRepository.CreateInvoiceItemAsync(dto);
            return CreatedAtAction(nameof(GetInvoiceItemsByInvoiceId), new { invoiceId = item.InvoiceId }, item);
        }

        // Get all items for a specific invoice
        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            var items = await _invoiceItemRepository.GetInvoiceItemsByInvoiceIdAsync(invoiceId);
            return Ok(items);
        }

        // Delete a specific invoice item
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceItem(int id)
        {
            var deleted = await _invoiceItemRepository.DeleteInvoiceItemAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
