using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthWindAPI101.Models;
using NorthWindAPI101.Models.DTO;
using NorthWindAPI101.Models.Services;
using RiskFirst.Hateoas;

namespace NorthWindAPI101
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<NorthwindContext>(
                opt => opt.UseSqlServer(builder.Configuration["default"]));

            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            




            //builder.Services.AddLink(builder => builder
            //        .AddPolicy<EmployeeDTO>(model =>
            //        {
            //            model.AddSelf(e => e.EmployeeId, "Shows employee info");
            //            model.AddCustomPath(e => $"/api/employees/{e.EmployeeId}", "UpdateEmployee", method: HttpMethods.Put, message: "Update the details for an employee");
            //            model.AddCustomPath(e => $"/api/employees/{e.EmployeeId}", "DeleteEmployee", method: HttpMethods.Delete, message: "Delete an employee from the database");
            //            model.AddCustomPath(e => $"/api/employees/{e.EmployeeId}/photo", "GetEmployeePhoto", method: HttpMethods.Get, message: "Shows the employee's photo as a jpeg image");
            //        }));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}