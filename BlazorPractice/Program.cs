using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorPractice.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BlazorPractice
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddDbContext<AppDataContext>(options =>
                options.UseSqlServer(@"Server=.\MSSQLDEV;Database=BasicDataCheck;Trusted_Connection=True;MultipleActiveResultSets=true"));


            //builder.RootComponents.Add<App>("app");

            var host = builder.Build();
            var customers = host.Services.GetService<AppDataContext>();
            //Console.WriteLine(customers.ToListAsync());

            //var result = await customers.Customer.;
            //Console.WriteLine(result);

            //var customersList = await customers.ToListAsync();
            //var firstCustomer = await customers.FirstOrDefaultAsync();

            //Console.WriteLine($"{firstCustomer.FirstName}{firstCustomer.Lastname}");

            //host.Configuration
            

            await builder.Build().RunAsync();
        }
    }
}
