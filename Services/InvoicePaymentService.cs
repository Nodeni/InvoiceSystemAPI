using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Services
{
    public class InvoicePaymentService : IInvoicePaymentService
    {
        private readonly AppDbContext _context;

        public InvoicePaymentService(AppDbContext context)
        {
            _context = context;
        }

        // Add a new payment and update invoice status if fully paid
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
    }
}
