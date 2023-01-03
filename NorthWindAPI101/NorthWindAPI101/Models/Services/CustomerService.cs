using NorthWindAPI101.Controllers;
using NorthWindAPI101.Models.DTO;

namespace NorthWindAPI101.Models.Services
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

        public void CreateCustomer(Customer c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(string customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public CustomerDTO GetCustomerDTOById(string customerId)
        {
            return _context.Customers.Where(x => x.CustomerId == customerId).Select(sel => Utils.CustomerToDTO(sel)).First();
        }

        public List<CustomerDTO> GetCustomerDTOList()
        {
            return _context.Customers.Select(sel=>Utils.CustomerToDTO(sel)).ToList();
        }

        public void RemoveCustomer(Customer c)
        {
            _context.Remove(c);
            _context.SaveChanges();
        }

        public void SaveCustomerChanges()
        {
            _context.SaveChanges();
        }

        public bool DoesCustomerExist(string id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
