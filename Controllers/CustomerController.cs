using Microsoft.AspNetCore.Mvc;
using SimpleRESTServerAPI.Services;
using static SimpleRESTServerAPI.Services.CustomerService;

namespace SimpleRESTServerAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {
        }

        [HttpPost(Name = "InsertCustomer")]
        public ActionResult Post([FromBody] List<Customer> newCustomers)
        {
            new CustomerService().InsertCustomer(newCustomers);
            return Ok("Customers added successfully");
        }

        [HttpGet(Name = "GetCustomers")]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return new CustomerService().ListCustomers();
        }
    }
}
