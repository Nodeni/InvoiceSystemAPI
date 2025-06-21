using Microsoft.AspNetCore.Mvc;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicePaymentController : ControllerBase
    {
        private readonly IInvoicePaymentService _invoicePaymentService;

        public InvoicePaymentController(IInvoicePaymentService invoicePaymentService)
        {
            _invoicePaymentService = invoicePaymentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(InvoicePaymentCreateDTO dto)
        {
            var payment = await _invoicePaymentService.AddPaymentAsync(dto);
            return CreatedAtAction(nameof(AddPayment), new { id = payment.Id }, payment);
        }

        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetPaymentsByInvoiceId(int invoiceId)
        {
            var payments = await _invoicePaymentService.GetPaymentsByInvoiceIdAsync(invoiceId);
            if (!payments.Any())
                return NotFound();

            return Ok(payments);
        }
    }
}
