using Microsoft.AspNetCore.Mvc;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(InvoiceCreateDTO dto)
        {
            var response = await _invoiceService.CreateInvoiceWithResponseAsync(dto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var dto = await _invoiceService.GetInvoiceDetailsByIdAsync(id);
            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetInvoicesByUser(int userId)
        {
            var result = await _invoiceService.GetInvoiceListByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceUpdateDTO dto)
        {
            var updated = await _invoiceService.UpdateInvoiceAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var deleted = await _invoiceService.DeleteInvoiceAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/with-payments")]
        public async Task<IActionResult> GetInvoiceWithPayments(int id)
        {
            var result = await _invoiceService.GetInvoiceWithPaymentsAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
