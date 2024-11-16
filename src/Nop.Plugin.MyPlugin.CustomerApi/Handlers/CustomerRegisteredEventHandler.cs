using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;
using Nop.Plugin.MyPlugin.CustomerApi.Services;
using Nop.Services.Events;

namespace Nop.Plugin.MyPlugin.CustomerApi.Handlers;
public class CustomerRegisteredEventHandler : IConsumer<CustomerRegisteredEvent>
{
    private readonly CustomerApiService _customerApiService;

    public CustomerRegisteredEventHandler(CustomerApiService customerApiService)
    {
        _customerApiService = customerApiService;
    }

    public async Task HandleEventAsync(CustomerRegisteredEvent eventMessage)
    {
        if ( await _customerApiService.ContactExistsAsync(eventMessage.Customer.Email))
        {
            throw new Exception("A contact with this email already exists in the CRM.");
        }

        await _customerApiService.CreateContactAsync(
            eventMessage.Customer.FirstName,
            eventMessage.Customer.LastName,
            eventMessage.Customer.Email);
    }
}