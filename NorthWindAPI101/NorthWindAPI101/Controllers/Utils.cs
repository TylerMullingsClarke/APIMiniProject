using NorthWindAPI101.Models;
using NorthWindAPI101.Models.DTO;

namespace NorthWindAPI101.Controllers
{
    public class Utils
    {

        public static CustomerDTO CustomerToDTO(Customer customer)
            => new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                City = customer.City,
                Country = customer.Country,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                Fax = customer.Fax,
                PostalCode= customer.PostalCode,
                Phone = customer.Phone,
                Region= customer.Region,
                // Setting the products property
                //Products = customer.Products.Select(x => ProductDTO(x)).ToList()                
            };

        public static ProductDTO ProductToDTO(Product product)
            => new ProductDTO
            {
                ProductId = product.ProductId,  
                ProductName = product.ProductName,
                SupplierId= product.SupplierId,
                CategoryId= product.CategoryId,
                UnitPrice = (decimal)product.UnitPrice

            };


    }
}
