using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Plugin.MyPlugin.CustomerApi.Handlers;
using Nop.Plugin.MyPlugin.CustomerApi.Services;
using Nop.Services.Events;
using Nop.Services.Plugins;

namespace Nop.Plugin.MyPlugin.CustomerApi;

public class CustomerApiPlugin : BasePlugin, INopStartup
{
    public int Order => 1;

    public override async Task InstallAsync()
    {
         await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await base.UninstallAsync();
    }


    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            string crmConnectionString = configuration.GetConnectionString("CrmConnection");

            if (string.IsNullOrEmpty(crmConnectionString))
            {
                throw new Exception("CRM Connection string is missing or invalid.");
            }

            services.AddScoped<ICustomerApiService>(provider => new CustomerApiService(crmConnectionString));
            services.AddScoped<IConsumer<CustomerRegisteredEvent>, CustomerRegisteredEventHandler>();

            Console.WriteLine("Services registered successfully for CRM connection.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ConfigureServices: {ex.Message}");
        }
    }

    public void Configure(IApplicationBuilder application)
    {

    }
}
