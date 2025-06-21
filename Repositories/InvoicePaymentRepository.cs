using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.IRepositories;
using Microsoft.EntityFrameworkCore;
using InvoiceSystemAPI.IServices;

namespace InvoiceSystemAPI.Repositories
{
    public class InvoicePaymentRepository : IInvoicePaymentRepository
    {
        private readonly AppDbContext _context;
        private readonly IInvoicePaymentService _paymentService;

        public InvoicePaymentRepository(AppDbContext context, IInvoicePaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        // Save a new payment for an invoice
        public async Task<InvoicePayment> AddPaymentAsync(InvoicePaymentCreateDTO dto)
        {
            return await _paymentService.AddPaymentAsync(dto);
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
