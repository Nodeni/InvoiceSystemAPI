using InvoiceSystemAPI.Data;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IRepositories;
using InvoiceSystemAPI.IServices;
using InvoiceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemAPI.Services
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly IInvoiceItemRepository _repository;

        public InvoiceItemService(IInvoiceItemRepository repository)
        {
            _repository = repository;
        }

        // Create and save a new invoice item
        public async Task<InvoiceItemDetailsDTO> CreateInvoiceItemAsync(InvoiceItemCreateDTO dto)
        {
            var item = new InvoiceItem
            {
                InvoiceId = dto.InvoiceId,
                Description = dto.Description,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice
            };

            var created = await _repository.CreateInvoiceItemAsync(item);

            return new InvoiceItemDetailsDTO
            {
                Id = created.Id,
                Description = created.Description,
                Quantity = created.Quantity,
                UnitPrice = created.UnitPrice,
                Total = created.Quantity * created.UnitPrice
            };
        }

        // Get all items for a specific invoice
        public async Task<IEnumerable<InvoiceItemDetailsDTO>> GetInvoiceItemsByInvoiceIdAsync(int invoiceId)
        {
            var items = await _repository.GetInvoiceItemsByInvoiceIdAsync(invoiceId);

            return items.Select(x => new InvoiceItemDetailsDTO
            {
                Id = x.Id,
                Description = x.Description,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Total = x.Quantity * x.UnitPrice
            });
        }

        // Delete a single invoice item
        public async Task<bool> DeleteInvoiceItemAsync(int id)
        {
            return await _repository.DeleteInvoiceItemAsync(id);
        }

        //Update invoice item
        public async Task<InvoiceItemDetailsDTO?> UpdateInvoiceItemAsync(int id, InvoiceItemUpdateDTO dto)
        {
            var updated = await _repository.UpdateInvoiceItemAsync(id, new InvoiceItem
            {
                Description = dto.Description,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice
            });

            if (updated == null) return null;

            return new InvoiceItemDetailsDTO
            {
                Id = updated.Id,
                Description = updated.Description,
                Quantity = updated.Quantity,
                UnitPrice = updated.UnitPrice,
                Total = updated.Quantity * updated.UnitPrice
            };
        }
    }
}
