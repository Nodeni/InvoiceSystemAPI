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

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceCreateDTO dto)
        {
            var invoice = await _invoiceRepository.CreateInvoiceAsync(dto);

            var response = new InvoiceResponseDTO
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                SubTotal = invoice.SubTotal,
                VAT = invoice.VAT,
                Total = invoice.Total,
                Status = invoice.Status,
                CustomerName = invoice.Customer.CompanyName ?? $"{invoice.Customer.FirstName} {invoice.Customer.LastName}",
                CustomerEmail = invoice.Customer.Email,
                UserOrganization = invoice.User.OrganizationName,
                UserEmail = invoice.User.Email
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var dto = await _invoiceRepository.GetInvoiceDetailsByIdAsync(id);

            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesByUserAsync(1); //This is only for testing purpose

            var result = invoices.Select(i => new InvoiceListDTO
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                IssueDate = i.IssueDate,
                DueDate = i.DueDate,
                Total = i.Total,
                Status = i.Status,
                CustomerName = i.Customer.CompanyName ?? $"{i.Customer.FirstName} {i.Customer.LastName}"
            });

            return Ok(result);
        }
    }
}
