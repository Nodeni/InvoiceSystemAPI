using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Services
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly AppDbContext _context;

        public InvoiceItemService(AppDbContext context)
        {
            _context = context;
        }

        // Create and save a new invoice item
        public async Task<InvoiceItem> CreateInvoiceItemAsync(InvoiceItemCreateDTO dto)
        {
            var invoiceItem = new InvoiceItem
            {
                InvoiceId = dto.InvoiceId,
                Description = dto.Description,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice
            };

            _context.InvoiceItems.Add(invoiceItem);
            await _context.SaveChangesAsync();

            return invoiceItem;
        }

        // Get all items for a specific invoice
        public async Task<IEnumerable<InvoiceItemDetailsDTO>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoiceItems
                .Where(item => item.InvoiceId == invoiceId)
                .Select(item => new InvoiceItemDetailsDTO
                {
                    Id = item.Id,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Quantity * item.UnitPrice
                })
                .ToListAsync();
        }

        // Delete a single invoice item
        public async Task<bool> DeleteInvoiceItemAsync(int id)
        {
            var item = await _context.InvoiceItems.FindAsync(id);
            if (item == null)
                return false;

            _context.InvoiceItems.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
