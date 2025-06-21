using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.Models;
using InvoiceSystemAPI.IRepositories;
using Microsoft.EntityFrameworkCore;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Services;

namespace InvoiceSystemAPI.Repositories
{
    public class InvoicePaymentRepository : IInvoicePaymentRepository
    {
        private readonly AppDbContext _context;
        private readonly IInvoicePaymentService _invoicePaymentService;

        public InvoicePaymentRepository(AppDbContext context, IInvoicePaymentService invoicePaymentService)
        {
            _context = context;
            _invoicePaymentService = invoicePaymentService;
        }

        // Save a new payment for an invoice
        public async Task<InvoicePayment> AddPaymentAsync(InvoicePaymentCreateDTO dto)
        {
            return await _invoicePaymentService.AddPaymentAsync(dto);
        }

        // Get all payments related to a specific invoice
        public async Task<IEnumerable<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId)
        {
            return await _invoicePaymentService.GetPaymentsByInvoiceIdAsync(invoiceId);
        }
    }
}
