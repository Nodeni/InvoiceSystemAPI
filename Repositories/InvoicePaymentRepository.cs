using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.IRepositories;
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

        // Save a new payment for an invoice
        public async Task<InvoicePayment> AddPaymentAsync(InvoicePaymentCreateDTO dto)
        {
            var payment = new InvoicePayment
            {
                InvoiceId = dto.InvoiceId,
                AmountPaid = dto.AmountPaid,
                PaidDate = dto.PaidDate
            };

            await _context.InvoicePayments.AddAsync(payment);
            await _context.SaveChangesAsync();

            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == dto.InvoiceId);

            if (invoice != null)
            {
                var totalPaid = await _context.InvoicePayments
                    .Where(p => p.InvoiceId == dto.InvoiceId)
                    .SumAsync(p => p.AmountPaid ?? 0);

                if (totalPaid >= invoice.Total)
                {
                    invoice.Status = "Paid";

                    payment.IsPaid = true;

                    _context.Invoices.Update(invoice);
                    await _context.SaveChangesAsync();
                }
            }

            return payment;
        }


        // Get all payments related to a specific invoice
        public async Task<IEnumerable<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoicePayments
                .Where(p => p.InvoiceId == invoiceId)
                .ToListAsync();
        }
    }
}
