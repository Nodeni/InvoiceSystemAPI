using InvoiceSystemAPI.Data;
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

        // Creates a new invoice and saves it to the database
        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        // Retrieves all invoices including related customer data
        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .ToListAsync();
        }

        // Retrieves all invoices belonging to a specific user, including customer data
        public async Task<IEnumerable<Invoice>> GetAllInvoicesByUserAsync(int userId)
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        // Retrieves a single invoice by its ID (no includes)
        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices.FindAsync(id);
        }

        // Updates an existing invoice in the database
        public async Task<bool> UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            return await _context.SaveChangesAsync() > 0;
        }

        // Deletes an invoice by ID if it exists
        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return false;
            _context.Invoices.Remove(invoice);
            return await _context.SaveChangesAsync() > 0;
        }

        // Retrieves one invoice with related customer, user, and items
        public async Task<Invoice?> GetInvoiceWithCustomerAndUserAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.User)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        // Retrieves all invoice items related to a specific invoice
        public async Task<List<InvoiceItem>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoiceItems
                .Where(ii => ii.InvoiceId == invoiceId)
                .ToListAsync();
        }

        // Retrieves all payments related to a specific invoice
        public async Task<List<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoicePayments
                .Where(p => p.InvoiceId == invoiceId)
                .ToListAsync();
        }
    }
}
