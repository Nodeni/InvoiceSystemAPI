using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Repositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly AppDbContext _context;

        public InvoiceItemRepository(AppDbContext context)
        {
            _context = context;
        }

        // Create a new invoice item and save to database
        public async Task<InvoiceItem> CreateInvoiceItemAsync(InvoiceItem item)
        {
            _context.InvoiceItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // Get all items belonging to a specific invoice
        public async Task<IEnumerable<InvoiceItem>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoiceItems
                .Where(x => x.InvoiceId == invoiceId)
                .ToListAsync();
        }

        //Update a specific invoice item
        public async Task<InvoiceItem?> UpdateInvoiceItemAsync(int id, InvoiceItem item)
        {
            var existing = await _context.InvoiceItems.FindAsync(id);
            if (existing == null) return null;

            existing.Description = item.Description;
            existing.Quantity = item.Quantity;
            existing.UnitPrice = item.UnitPrice;

            await _context.SaveChangesAsync();
            return existing;
        }

        // Delete one specific invoice item
        public async Task<bool> DeleteInvoiceItemAsync(int id)
        {
            var item = await _context.InvoiceItems.FindAsync(id);
            if (item == null) return false;

            _context.InvoiceItems.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
