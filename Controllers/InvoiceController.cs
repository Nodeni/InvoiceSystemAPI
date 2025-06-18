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
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.User)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
                return NotFound();

            var items = await _context.InvoiceItems
                .Where(ii => ii.InvoiceId == id)
                .Select(ii => new InvoiceItemDetailsDTO
                {
                    Description = ii.Description,
                    Quantity = ii.Quantity,
                    UnitPrice = ii.UnitPrice,
                    Total = ii.Total
                }).ToListAsync();

            var dto = new InvoiceDetailsDTO
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
                CustomerAddress = invoice.Customer.AddressLine1,
                CustomerZipCode = invoice.Customer.ZipCode,
                CustomerCity = invoice.Customer.City,
                CustomerCountry = invoice.Customer.Country,
                CustomerOrgNr = invoice.Customer.OrganizationNumber,

                CompanyName = invoice.User.OrganizationName,
                CompanyEmail = invoice.User.Email,
                CompanyAddress = invoice.User.AddressLine1,
                CompanyZipCode = invoice.User.ZipCode,
                CompanyCity = invoice.User.City,
                CompanyCountry = invoice.User.Country,
                Bankgiro = invoice.User.Bankgiro,
                IBAN = invoice.User.IBAN,
                SwishNumber = invoice.User.SwishNumber,

                Items = items
            };

            return Ok(dto);
        }

    }
}
