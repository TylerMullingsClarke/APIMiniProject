using Microsoft.AspNetCore.Mvc;

namespace NorthwindCustomerAPI.Models.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomerListAsync();
        Task<Customer> GetCustomerByIdAsync(string customerId);
        Task CreateCustomerAsync(Customer c);
        Task SaveCustomerChangesAsync();
        Task RemoveCustomerAsync(Customer c);
        Task ModifyCustomerAsync(Customer c);
    }
}
