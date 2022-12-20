using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace NorthwindCustomerAPI.Models.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly NorthwindContext _context;

        public CustomerService()
        {
            _context = new NorthwindContext();
        }

        public CustomerService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerAsync(Customer c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }

        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public List<Customer> GetCustomerListAsync()
        {
            return _context.Customers.ToList();
        }

        public async Task RemoveCustomerAsync(Customer c)
        {
            _context.Remove(c);
            _context.SaveChanges();
        }

        public async Task SaveCustomerChangesAsync()
        {
             _context.SaveChanges();
        }

        public async Task ModifyCustomerAsync(Customer c)
        {
            _context.Entry(c).State = EntityState.Modified;
        }
    }
}
