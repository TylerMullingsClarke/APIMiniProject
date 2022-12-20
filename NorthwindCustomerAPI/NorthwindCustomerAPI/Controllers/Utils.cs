using NorthwindCustomerAPI.Models;
using NorthwindCustomerAPI.Models.DTO;

namespace NorthwindCustomerAPI.Controllers
{
    public class Utils
    {
        public static CustomerDTO CustomerToDTO(Customer customer) =>
            new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                Address = customer.Address,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax,
                Orders = customer.Orders.Select(o=>OrderToDTO(o)).ToList()
            };
        public static OrderDTO OrderToDTO(Order order) =>
            new OrderDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipAddress = order.ShipAddress,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry,
            };
    }
}
