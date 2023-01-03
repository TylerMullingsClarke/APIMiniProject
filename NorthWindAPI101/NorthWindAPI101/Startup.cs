using NorthWindAPI101.Models.DTO;
using RiskFirst;
using RiskFirst.Hateoas;

namespace NorthWindAPI101
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLinks(config =>
            {
                config.AddPolicy<CustomerDTO>(policy => {
                    policy.RequireSelfLink()
                          .RequireRoutedLink("all", "GetAllCustomersRoute")
                          .RequireRoutedLink("delete", "DeleteCustomerRoute", x => new { id = x.CustomerId });
                });
            });
        }
    }
}
