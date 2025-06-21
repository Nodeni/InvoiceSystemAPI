using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        // Creates a new invoice with items and calculates total, VAT, etc.
        public async Task<InvoiceResponseDTO> CreateInvoiceAsync(InvoiceCreateDTO dto)
        {
            var invoice = new Invoice
            {
                CustomerId = dto.CustomerId,
                UserId = dto.UserId,
                DueDate = dto.DueDate,
                IssueDate = DateTime.UtcNow,
                Status = "Unpaid",
                InvoiceNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                Items = dto.Items.Select(item => new InvoiceItem
                {
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Quantity * item.UnitPrice
                }).ToList()
            };

            invoice.SubTotal = invoice.Items.Sum(i => i.Total);
            invoice.VAT = invoice.SubTotal * 0.25m;
            invoice.Total = invoice.SubTotal + invoice.VAT;

            var created = await _invoiceRepository.CreateInvoiceAsync(invoice);

            var fullInvoice = await _invoiceRepository.GetInvoiceWithCustomerAndUserAsync(created.Id);

            return new InvoiceResponseDTO
            {
                Id = fullInvoice!.Id,
                InvoiceNumber = fullInvoice.InvoiceNumber,
                IssueDate = fullInvoice.IssueDate,
                DueDate = fullInvoice.DueDate,
                SubTotal = fullInvoice.SubTotal,
                VAT = fullInvoice.VAT,
                Total = fullInvoice.Total,
                Status = fullInvoice.Status,
                CustomerName = fullInvoice.Customer.CompanyName ?? $"{fullInvoice.Customer.FirstName} {fullInvoice.Customer.LastName}",
                CustomerEmail = fullInvoice.Customer.Email,
                UserOrganization = fullInvoice.User.OrganizationName,
                UserEmail = fullInvoice.User.Email
            };
        }

        // Creates invoice and returns a mapped response
        public async Task<InvoiceResponseDTO> CreateInvoiceWithResponseAsync(InvoiceCreateDTO dto)
        {
            return await CreateInvoiceAsync(dto);
        }

        // Gets all invoices with customer info as DTO
        public async Task<IEnumerable<InvoiceListDTO>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();
            return invoices.Select(i => new InvoiceListDTO
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                IssueDate = i.IssueDate,
                DueDate = i.DueDate,
                Total = i.Total,
                Status = i.Status,
                CustomerName = i.Customer.CompanyName ?? $"{i.Customer.FirstName} {i.Customer.LastName}"
            });
        }

        // Gets invoice list for a specific user
        public async Task<IEnumerable<InvoiceListDTO>> GetInvoiceListByUserIdAsync(int userId)
        {
            var invoices = await _invoiceRepository.GetAllInvoicesByUserAsync(userId);
            return invoices.Select(i => new InvoiceListDTO
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                IssueDate = i.IssueDate,
                DueDate = i.DueDate,
                Total = i.Total,
                Status = i.Status,
                CustomerName = i.Customer.CompanyName ?? $"{i.Customer.FirstName} {i.Customer.LastName}"
            });
        }

        // Gets full invoice details including customer, company, and items
        public async Task<InvoiceDetailsDTO?> GetInvoiceDetailsByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetInvoiceWithCustomerAndUserAsync(id);
            if (invoice == null) return null;

            var items = invoice.Items.Select(ii => new InvoiceItemDetailsDTO
            {
                Description = ii.Description,
                Quantity = ii.Quantity,
                UnitPrice = ii.UnitPrice,
                Total = ii.Total
            }).ToList();

            return new InvoiceDetailsDTO
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                SubTotal = invoice.SubTotal,
                VAT = invoice.VAT,
                Total = invoice.Total,
                Status = invoice.Status,

                CustomerName = invoice.Customer.CompanyName ?? $"{invoice.Customer.FirstName} {invoice.Customer.LastName}",
                CustomerEmail = invoice.Customer.Email,
                CustomerAddress = invoice.Customer.AddressLine1,
                CustomerZipCode = invoice.Customer.ZipCode,
                CustomerCity = invoice.Customer.City,
                CustomerCountry = invoice.Customer.Country,
                CustomerOrgNr = invoice.Customer.OrganizationNumber,

                CompanyName = invoice.User.OrganizationName,
                CompanyEmail = invoice.User.Email,
                CompanyAddress = invoice.User.AddressLine1,
                CompanyZipCode = invoice.User.ZipCode,
                CompanyCity = invoice.User.City,
                CompanyCountry = invoice.User.Country,
                Bankgiro = invoice.User.Bankgiro,
                IBAN = invoice.User.IBAN,
                SwishNumber = invoice.User.SwishNumber,

                Items = items
            };
        }

        // Updates invoice due date and status
        public async Task<bool> UpdateInvoiceAsync(int id, InvoiceUpdateDTO dto)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
            if (invoice == null) return false;

            invoice.DueDate = dto.DueDate;
            invoice.Status = dto.Status;

            return await _invoiceRepository.UpdateInvoiceAsync(invoice);
        }

        // Deletes an invoice if it exists
        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            return await _invoiceRepository.DeleteInvoiceAsync(id);
        }

        // Gets full invoice details + all associated payments
        public async Task<InvoiceWithPaymentsDTO?> GetInvoiceWithPaymentsAsync(int invoiceId)
        {
            var invoiceDto = await GetInvoiceDetailsByIdAsync(invoiceId);
            if (invoiceDto == null) return null;

            var payments = await _invoiceRepository.GetPaymentsByInvoiceIdAsync(invoiceId);

            return new InvoiceWithPaymentsDTO
            {
                Invoice = invoiceDto,
                Payments = payments
            };
        }
    }
}
