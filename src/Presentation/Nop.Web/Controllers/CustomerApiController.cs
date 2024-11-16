using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.MyPlugin.CustomerApi.Services;
using Nop.Services.Customers;
using Nop.Web.Framework.Controllers;
using Nop.Web.Models.Common;
namespace Nop.Web.Controllers;

[Route("api/myplugin/customerapi")]
public class CustomerApiController : BasePluginController
{
    private readonly ICustomerApiService _customerApiService;

    public CustomerApiController(ICustomerApiService customerApiService)
    {
        _customerApiService = customerApiService;
    }

    [HttpGet("contact-exists")]
    public async Task<IActionResult> ContactExists(string email)
    {
        if (string.IsNullOrEmpty(email))
            return BadRequest("Email is required");

        var exists = await _customerApiService.ContactExistsAsync(email);

        return Ok(new { exists });
    }

    [HttpPost("create-contact")]
    public async Task<IActionResult> CreateContact([FromBody] ContactModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _customerApiService.CreateContactAsync(model.FirstName, model.LastName, model.Email);

        return Ok(new { message = "Contact created successfully" });
    }

    public class ContactModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
