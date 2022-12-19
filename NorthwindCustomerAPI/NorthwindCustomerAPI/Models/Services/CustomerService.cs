namespace NorthwindCustomerAPI.Models.Services
{
    public class CustomerService
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

        public List<Customer> GetCustomerList()
        {
            return _context.Customers.ToList();
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
    }
}
