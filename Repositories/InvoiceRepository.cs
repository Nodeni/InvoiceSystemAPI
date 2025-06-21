using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
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
        private readonly IInvoiceService _invoiceService;

        public InvoiceRepository(AppDbContext context, IInvoiceService invoiceService)
        {
            _context = context;
            _invoiceService = invoiceService;
        }

        // Create a new invoice
        public async Task<InvoiceResponseDTO> CreateInvoiceAsync(InvoiceCreateDTO dto)
        {
            return await _invoiceService.CreateInvoiceAsync(dto);
        }

        // Get all invoices created by a specific user
        public async Task<IEnumerable<Invoice>> GetAllInvoicesByUserAsync(int userId)
        {
            return await _invoiceService.GetAllInvoicesByUserAsync(userId);
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

        // Get all invoice list items for a specific user
        public async Task<IEnumerable<InvoiceListDTO>> GetInvoiceListByUserIdAsync(int userId)
        {
            return await _invoiceService.GetInvoiceListByUserIdAsync(userId);
        }
    }
}
