using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.MyPlugin.CustomerApi.Services;
public interface ICustomerApiService
{
    Task<bool> ContactExistsAsync(string email);
    Task CreateContactAsync(string firstName, string lastName, string email);
}
