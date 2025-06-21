using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IInvoiceService _invoiceService;

        public InvoiceRepository(AppDbContext context, IInvoiceService invoiceService)
        {
            _context = context;
            _invoiceService = invoiceService;
        }

        // Create a new invoice
        public async Task<InvoiceResponseDTO> CreateInvoiceAsync(InvoiceCreateDTO dto)
        {
            return await _invoiceService.CreateInvoiceAsync(dto);
        }

        // Get all invoices created by a specific user
        public async Task<IEnumerable<Invoice>> GetAllInvoicesByUserAsync(int userId)
        {
            return await _invoiceService.GetAllInvoicesByUserAsync(userId);
        }

        // Get detailed info for a specific invoice by ID
        public async Task<InvoiceDetailsDTO?> GetInvoiceDetailsByIdAsync(int id)
        {
            return await _invoiceService.GetInvoiceDetailsByIdAsync(id);
        }

        // Update due date and status of an invoice
        public async Task<bool> UpdateInvoiceAsync(int id, InvoiceUpdateDTO dto)
        {
            return await _invoiceService.UpdateInvoiceAsync(id, dto);
        }

        // Delete an invoice from the database
        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            return await _invoiceService.DeleteInvoiceAsync(id);
        }

        // Get a specific invoice and include all its related payments
        public async Task<InvoiceWithPaymentsDTO?> GetInvoiceWithPaymentsAsync(int invoiceId)
        {
            return await _invoiceService.GetInvoiceWithPaymentsAsync(invoiceId);
        }

        // Get all invoice list items for a specific user
        public async Task<IEnumerable<InvoiceListDTO>> GetInvoiceListByUserIdAsync(int userId)
        {
            return await _invoiceService.GetInvoiceListByUserIdAsync(userId);
        }

        // Create invoice and return mapped response DTO
        public async Task<InvoiceResponseDTO> CreateInvoiceWithResponseAsync(InvoiceCreateDTO dto)
        {
            return await _invoiceService.CreateInvoiceWithResponseAsync(dto);
        }

        // Get all invoices with customer info
        public async Task<IEnumerable<InvoiceListDTO>> GetAllInvoicesAsync()
        {
            return await _invoiceService.GetAllInvoicesAsync();
        }
    }
}
