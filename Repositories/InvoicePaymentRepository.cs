using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Repositories
{
    public class InvoicePaymentRepository : IInvoicePaymentRepository
    {
        private readonly AppDbContext _context;

        public InvoicePaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        // Save a new payment
        public async Task<InvoicePayment> AddPaymentAsync(InvoicePayment payment)
        {
            _context.InvoicePayments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        // Get all payments for a specific invoice
        public async Task<IEnumerable<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoicePayments
                .Where(p => p.InvoiceId == invoiceId)
                .ToListAsync();
        }

        // Calculate total amount paid for a specific invoice
        public async Task<decimal> GetTotalPaidAmountAsync(int invoiceId)
        {
            return await _context.InvoicePayments
                .Where(p => p.InvoiceId == invoiceId)
                .SumAsync(p => p.AmountPaid ?? 0);
        }

        // Update invoice status
        public async Task UpdateInvoiceStatusAsync(int invoiceId, bool isPaid)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice != null)
            {
                invoice.Status = isPaid ? "Paid" : invoice.Status;
                _context.Invoices.Update(invoice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
