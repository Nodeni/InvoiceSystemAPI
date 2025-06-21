using Microsoft.AspNetCore.Mvc;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceItemController : ControllerBase
    {
        private readonly IInvoiceItemService _invoiceItemService;

        public InvoiceItemController(IInvoiceItemService invoiceItemService)
        {
            _invoiceItemService = invoiceItemService;
        }

        // POST: Create a new invoice item
        [HttpPost]
        public async Task<IActionResult> CreateInvoiceItem([FromBody] InvoiceItemCreateDTO dto)
        {
            var item = await _invoiceItemService.CreateInvoiceItemAsync(dto);
            return CreatedAtAction(nameof(GetInvoiceItemsByInvoiceId), new { invoiceId = item.InvoiceId }, item);
        }

        // GET: Get all items for a specific invoice
        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            var items = await _invoiceItemService.GetInvoiceItemsByInvoiceIdAsync(invoiceId);
            return Ok(items);
        }

        // PUT: Update a specific invoice item
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoiceItem(int id, [FromBody] InvoiceItemUpdateDTO dto)
        {
            var updated = await _invoiceItemService.UpdateInvoiceItemAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: Delete a specific invoice item
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceItem(int id)
        {
            var deleted = await _invoiceItemService.DeleteInvoiceItemAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
