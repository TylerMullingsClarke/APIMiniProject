using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Graph;
using NorthWindAPI101.Models;
using NorthWindAPI101.Models.DTO;
using NorthWindAPI101.Models.Services;
using NPOI.SS.Formula.Functions;
using NuGet.Protocol.Core.Types;
using RiskFirst.Hateoas;

namespace NorthWindAPI101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private LinkGenerator _linkGenerator;
        private readonly ILinksService _linksService;


        public CustomersController(ICustomerService service, LinkGenerator linkGenerator, ILinksService linksService)
        {
            _service = service;
            _linkGenerator = linkGenerator;
            _linksService = linksService;

        }

        // GET: api/Customers // Updated version
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await Task.Run(()=>_service.GetCustomerDTOList());

            
            return customers;
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "GetModelRoute")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(string id)
        {
            if (!CustomerExists(id))
            {
                return NotFound();
            }

            var customer = await Task.Run(() => _service.GetCustomerDTOById(id));
            await _linksService.AddLinksAsync(customer);
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

    //    private IEnumerable<Link> CreateLinksForCustomer(string id)
    //    {
    //        var links = new List<Link>
    //{
    //    new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetCustomer), values: new { id }),
    //    "self",
    //    "GET"),
    //    new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteCustomer), values: new { id }),
    //    "delete_customer",
    //    "DELETE"),
    //    new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(PutCustomer), values: new { id }),
    //    "update_customer",
    //    "PUT")
    //};
    //        return links;
    //    }





    }
}
