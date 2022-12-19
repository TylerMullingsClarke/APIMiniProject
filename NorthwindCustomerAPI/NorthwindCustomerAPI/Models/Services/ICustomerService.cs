namespace NorthwindCustomerAPI.Models.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomerList();
        Customer GetCustomerById(string customerId);
        void CreateCustomer(Customer c);
        void SaveCustomerChanges();
        void RemoveCustomer(Customer c);
    }
}
