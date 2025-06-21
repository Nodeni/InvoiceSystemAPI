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

        // Get all items belonging to a specific invoice
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

        // Delete one specific invoice item
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
