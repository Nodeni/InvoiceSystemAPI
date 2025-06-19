using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        // Save a new invoice with its items to the database
        public async Task<Invoice> CreateInvoiceAsync(InvoiceCreateDTO dto)
        {
            var invoice = new Invoice
            {
                CustomerId = dto.CustomerId,
                UserId = dto.UserId,
                DueDate = dto.DueDate,
                IssueDate = DateTime.UtcNow,
                Status = "Unpaid", // Always status Unpaid when creating a new invoice
                InvoiceNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),

            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            decimal subTotal = 0;

            foreach (var itemDto in dto.Items)
            {
                var item = new InvoiceItem
                {
                    InvoiceId = invoice.Id,
                    Description = itemDto.Description,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    Total = itemDto.Quantity * itemDto.UnitPrice
                };

                subTotal += item.Total;

                _context.InvoiceItems.Add(item);
            }

            decimal vat = subTotal * 0.25m;
            decimal total = subTotal + vat;

            invoice.SubTotal = subTotal;
            invoice.VAT = vat;
            invoice.Total = total;

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
            await _context.Entry(invoice).Reference(i => i.Customer).LoadAsync();
            await _context.Entry(invoice).Reference(i => i.User).LoadAsync();

            return invoice;
        }

        // Create invoice and return mapped response DTO
        public async Task<InvoiceResponseDTO> CreateInvoiceWithResponseAsync(InvoiceCreateDTO dto)
        {
            var invoice = await CreateInvoiceAsync(dto);

            var customerName = invoice.Customer.CompanyName ?? $"{invoice.Customer.FirstName} {invoice.Customer.LastName}";

            return new InvoiceResponseDTO
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                SubTotal = invoice.SubTotal,
                VAT = invoice.VAT,
                Total = invoice.Total,
                Status = invoice.Status,
                CustomerName = customerName,
                CustomerEmail = invoice.Customer.Email,
                UserOrganization = invoice.User.OrganizationName,
                UserEmail = invoice.User.Email
            };
        }

        // Get all invoices created by a specific user
        public async Task<IEnumerable<Invoice>> GetAllInvoicesByUserAsync(int userId)
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        // Get detailed info for a specific invoice by ID
        public async Task<InvoiceDetailsDTO?> GetInvoiceDetailsByIdAsync(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.User)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
                return null;

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

            return dto;
        }

        // Get all invoices with customer info
        public async Task<IEnumerable<InvoiceListDTO>> GetAllInvoicesAsync()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .ToListAsync();

            return invoices.Select(i => new InvoiceListDTO
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                IssueDate = i.IssueDate,
                DueDate = i.DueDate,
                Total = i.Total,
                Status = i.Status,
                CustomerName = i.Customer.CompanyName ?? $"{i.Customer.FirstName} {i.Customer.LastName}"
            });
        }

        // Update due date and status of an invoice
        public async Task<bool> UpdateInvoiceAsync(int id, InvoiceUpdateDTO dto)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
                return false;

            invoice.DueDate = dto.DueDate;
            invoice.Status = dto.Status;

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete an invoice from the database
        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
                return false;

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return true;
        }

        // Get a specific invoice and include all its related payments
        public async Task<InvoiceWithPaymentsDTO?> GetInvoiceWithPaymentsAsync(int invoiceId)
        {
            var invoiceDto = await GetInvoiceDetailsByIdAsync(invoiceId);
            if (invoiceDto == null)
                return null;

            var payments = await _context.InvoicePayments
                .Where(p => p.InvoiceId == invoiceId)
                .ToListAsync();

            return new InvoiceWithPaymentsDTO
            {
                Invoice = invoiceDto,
                Payments = payments
            };
        }
    }
}
