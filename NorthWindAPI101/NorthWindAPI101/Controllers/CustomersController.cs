using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using NorthWindAPI101.Models;
using NorthWindAPI101.Models.DTO;
using NorthWindAPI101.Models.Services;
using Sciensoft.Hateoas;
using Sciensoft.Hateoas.Repositories;



namespace NorthWindAPI101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;


        public CustomersController(ICustomerService service)
        {
            _service = service;

        }

        // GET: api/Customers // Updated version
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await Task.Run(()=>_service.GetCustomerDTOList());

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

            var customer = await Task.Run(() => _service.GetCustomerDTOById(id));
            return customer;
            //return customer;
        }
        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, CustomerDTO customerDTO)
        {


            var customer = await Task.Run(() => _service.GetCustomerById(id));


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
                await Task.Run(() => _service.SaveCustomerChanges());
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
        public async Task<ActionResult<CustomerDTO[]>> PostMultipleCustomer(CustomerDTO[] customerDTOs)
        {
            Customer customer = new Customer { };

            foreach (CustomerDTO cust in customerDTOs)
            {
                customer.CustomerId = cust.CustomerId;
                customer.ContactName = cust.ContactName;
                customer.CompanyName = cust.CompanyName;
                customer.ContactTitle = cust.ContactTitle;
                customer.PostalCode = cust.PostalCode;
                customer.Address = cust.Address;
                customer.City = cust.City;
                customer.Country = cust.Country;
                customer.Phone = cust.Phone;
                customer.Fax = cust.Fax;
                customer.Region = cust.Region;

                await Task.Run(()=>_service.CreateCustomer(customer));

                var customerExists = await Task.Run(() => _service.GetCustomerById(customer.CustomerId));
                if (customerExists is null)
                {
                    return BadRequest();
                }
            }
            for (int i = 0; i < customerDTOs.Length; i++)
            {
                customerDTOs[i] = await Task.Run(() => _service.GetCustomerDTOById(customerDTOs[i].CustomerId));
            }
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customerDTOs);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await Task.Run(() => _service.GetCustomerById(id));
            if (customer == null)
            {
                return NotFound();
            }

            await Task.Run(() => _service.RemoveCustomer(customer));

            return NoContent();
        }

        private bool CustomerExists(string id)
        {
            return _service.DoesCustomerExist(id);
        }
    }
}
