namespace NorthwindCustomerAPI.Models.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }
    }
}
