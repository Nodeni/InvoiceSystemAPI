using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.IServices;
using Microsoft.EntityFrameworkCore;
using InvoiceSystemAPI.Data;

namespace InvoiceSystemAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly AppDbContext _context;

        public InvoiceService(IInvoiceRepository invoiceRepository, AppDbContext context)
        {
            _invoiceRepository = invoiceRepository;
            _context = context;
        }

        // Create a new invoice
        public async Task<InvoiceResponseDTO> CreateInvoiceAsync(InvoiceCreateDTO dto)
        {
            var invoice = new Invoice
            {
                CustomerId = dto.CustomerId,
                UserId = dto.UserId,
                DueDate = dto.DueDate,
                IssueDate = DateTime.UtcNow,
                Status = "Unpaid",
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
                CustomerName = invoice.Customer.CompanyName ?? $"{invoice.Customer.FirstName} {invoice.Customer.LastName}",
                CustomerEmail = invoice.Customer.Email,
                UserOrganization = invoice.User.OrganizationName,
                UserEmail = invoice.User.Email
            };

        }

        // Create invoice and return mapped response DTO
        public async Task<InvoiceResponseDTO> CreateInvoiceWithResponseAsync(InvoiceCreateDTO dto)
        {
            var invoice = await CreateInvoiceAsync(dto);

            var customerName = invoice.CustomerName;
            var customerEmail = invoice.CustomerEmail;
            var userEmail = invoice.UserEmail;
            var userOrganization = invoice.UserOrganization;

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
                CustomerEmail = customerEmail,
                UserOrganization = userOrganization,
                UserEmail = userEmail
            };
        }
    }
}
