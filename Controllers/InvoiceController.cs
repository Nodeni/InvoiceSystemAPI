using Microsoft.AspNetCore.Mvc;
using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(AppDbContext context, IInvoiceRepository invoiceRepository)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
        }

        // Create a new invoice
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceCreateDTO dto)
        {
            var response = await _invoiceRepository.CreateInvoiceWithResponseAsync(dto);
            return Ok(response);
        }

        // Get a specific invoice by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var dto = await _invoiceRepository.GetInvoiceDetailsByIdAsync(id);

            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        // Get all invoices in the system
        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        // Get all invoices for a specific user by their user ID
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetInvoicesByUser(int userId)
        {
            var result = await _invoiceRepository.GetInvoiceListByUserIdAsync(userId);
            return Ok(result);
        }

        // Update invoice status or due date
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceUpdateDTO dto)
        {
            var updated = await _invoiceRepository.UpdateInvoiceAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent(); // 204 = succesful update, no content needed as response
        }

        // Delete a specific invoice by its ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var deleted = await _invoiceRepository.DeleteInvoiceAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent(); // 204 = deletion successful
        }

        // Get a specific invoice and its related payments
        [HttpGet("{id}/with-payments")]
        public async Task<IActionResult> GetInvoiceWithPayments(int id)
        {
            var result = await _invoiceRepository.GetInvoiceWithPaymentsAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
