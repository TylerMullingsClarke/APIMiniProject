using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWindAPI101.Models;
using NorthWindAPI101.Models.DTO;

namespace NorthWindAPI101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CustomersController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Customers // Old version
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        //{
        //    return await _context.Customers.ToListAsync();
        //}

        // GET: api/Customers // Updated version
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _context.Customers
                //.Include(s => s.CustomerId)
                .Select(x => Utils.CustomerToDTO(x))
                .ToListAsync();

            return customers;
        }



        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(string id)
        {
            if (!CustomerExists(id))
            {
                return NotFound();
            }

            var customer = await _context.Customers.Where(x => x.CustomerId == id).Select(sel => Utils.CustomerToDTO(sel)).FirstAsync();

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, CustomerDTO customerDTO)
        {


            var customer = await _context.Customers.FindAsync(id);


            if (id != customerDTO.CustomerId || customer == null)
            {
                return BadRequest();
            }

            customer.ContactName = customerDTO.ContactName ?? customer.ContactName;
            customer.CompanyName = customerDTO.CompanyName ?? customer.CompanyName;
            customer.ContactTitle = customerDTO.ContactTitle ?? customer.ContactTitle;
            customer.PostalCode = customerDTO.PostalCode ?? customer.PostalCode;
            customer.Address = customerDTO.Address ?? customer.Address;
            customer.City = customerDTO.City ?? customer.City;
            customer.Country = customerDTO.Country ?? customer.Country;
            customer.Phone = customerDTO.Phone ?? customer.Phone;
            customer.Fax = customerDTO.Fax ?? customer.Fax;
            customer.Region = customerDTO.Region ?? customer.Region;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new Customer
            {
                CustomerId = customerDTO.CustomerId,
                ContactName = customerDTO.ContactName,
                CompanyName = customerDTO.CompanyName,
                ContactTitle = customerDTO.ContactTitle,
                PostalCode = customerDTO.PostalCode,
                Address = customerDTO.Address,
                City = customerDTO.City,
                Country = customerDTO.Country,
                Phone = customerDTO.Phone,
                Fax = customerDTO.Fax,
                Region = customerDTO.Region
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var customerExists = await _context.Customers.FindAsync(customer.CustomerId);
            if (customerExists is null)
            {
                return BadRequest();
            }

            customerDTO = await _context.Customers.Where(s => s.CustomerId == customer.CustomerId).Select(x => Utils.CustomerToDTO(x)).FirstAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customerDTO);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
