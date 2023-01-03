using RiskFirst.Hateoas.Models;

namespace NorthWindAPI101.Models.DTO
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
        }

        public string CustomerId { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string? ContactName { get; set; }

        public string? ContactTitle { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? Phone { get; set; }

        public string? Fax { get; set; }
    }



}
