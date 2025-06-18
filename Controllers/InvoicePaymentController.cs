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
    public class InvoicePaymentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IInvoicePaymentRepository _invoicePaymentRepository;

        public InvoicePaymentController(AppDbContext context, IInvoicePaymentRepository invoicePaymentRepository)
        {
            _context = context;
            _invoicePaymentRepository = invoicePaymentRepository;
        }

        // Add a new payment to an invoice
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] InvoicePaymentCreateDTO dto)
        {
            var payment = await _invoicePaymentRepository.AddPaymentAsync(dto);
            return CreatedAtAction(nameof(AddPayment), new { id = payment.Id }, payment);
        }

        // Get all payments for a specific invoice
        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetPaymentsByInvoiceId(int invoiceId)
        {
            var payments = await _invoicePaymentRepository.GetPaymentsByInvoiceIdAsync(invoiceId);

            if (!payments.Any())
                return NotFound();

            return Ok(payments);
        }

    }
}
