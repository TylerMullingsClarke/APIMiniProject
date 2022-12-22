using Microsoft.Extensions.DependencyInjection;
using NorthWindAPI101.Models.DTO;
using RiskFirst.Hateoas;

namespace NorthWindAPI101.Models
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLinks(config =>
            {
                config.AddPolicy<CustomerDTO>(policy => {
                    policy.RequireSelfLink()
                          .RequireRoutedLink("all", "GetAllModelsRoute")
                          .RequireRoutedLink("delete", "DeleteModelRoute", Customer => new { id = Customer.CustomerId });
                });
            });
        }
    }
}
