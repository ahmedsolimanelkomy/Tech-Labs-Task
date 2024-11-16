using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Nop.Plugin.MyPlugin.CustomerApi.Services
{
    public class CustomerApiService : ICustomerApiService
    {
        private readonly ServiceClient _crmServiceClient;

        public CustomerApiService(string connectionString)
        {
            _crmServiceClient = new ServiceClient(connectionString);
        }

        public async Task<bool> ContactExistsAsync(string email)
        {
            var query = new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet("emailaddress1")
            };
            query.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, email);

            var result = await Task.Run(() => _crmServiceClient.RetrieveMultiple(query));
            return result.Entities.Any();
        }

        public async Task CreateContactAsync(string firstName, string lastName, string email)
        {
            var contact = new Entity("contact")
            {
                ["firstname"] = firstName,
                ["lastname"] = lastName,
                ["emailaddress1"] = email
            };

            await Task.Run(() => _crmServiceClient.Create(contact));
        }
    }
}
