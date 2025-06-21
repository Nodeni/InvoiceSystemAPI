using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.Services
{
    public class InvoicePaymentService : IInvoicePaymentService
    {
        private readonly IInvoicePaymentRepository _invoicePaymentRepository;

        public InvoicePaymentService(IInvoicePaymentRepository invoicePaymentRepository)
        {
            _invoicePaymentRepository = invoicePaymentRepository;
        }

        // Add a new payment and update invoice status if needed
        public async Task<InvoicePayment> AddPaymentAsync(InvoicePaymentCreateDTO dto)
        {
            var payment = new InvoicePayment
            {
                InvoiceId = dto.InvoiceId,
                AmountPaid = dto.AmountPaid,
                PaidDate = dto.PaidDate
            };

            var savedPayment = await _invoicePaymentRepository.AddPaymentAsync(payment);

            var totalPaid = await _invoicePaymentRepository.GetTotalPaidAmountAsync(dto.InvoiceId);

            if (totalPaid >= savedPayment.Invoice?.Total)
            {
                payment.IsPaid = true;
                await _invoicePaymentRepository.UpdateInvoiceStatusAsync(dto.InvoiceId, true);
            }

            return savedPayment;
        }

        // Get all payments for an invoice
        public async Task<IEnumerable<InvoicePayment>> GetPaymentsByInvoiceIdAsync(int invoiceId)
        {
            return await _invoicePaymentRepository.GetPaymentsByInvoiceIdAsync(invoiceId);
        }
    }
}
