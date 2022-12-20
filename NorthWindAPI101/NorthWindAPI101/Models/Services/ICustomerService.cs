using NorthWindAPI101.Models.DTO;

namespace NorthWindAPI101.Models.Services
{
    public interface ICustomerService
    {
        List<CustomerDTO> GetCustomerDTOList();
        Customer GetCustomerById(string customerId);
        CustomerDTO GetCustomerDTOById(string customerId);
        void CreateCustomer(Customer c);
        void SaveCustomerChanges();
        void RemoveCustomer(Customer c);
    }
}
