using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
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
    }
}
