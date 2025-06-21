using Microsoft.AspNetCore.Mvc;
using InvoiceSystemAPI.DTOs;
using InvoiceSystemAPI.IServices;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerCreateDTO dto)
        {
            var customer = await _customerService.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCustomersByUserId(int userId)
        {
            var customers = await _customerService.GetCustomersByUserIdAsync(userId);
            if (!customers.Any())
                return NotFound();

            return Ok(customers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDTO dto)
        {
            var updated = await _customerService.UpdateCustomerAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteCustomerAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
