using Microsoft.AspNetCore.Mvc;
using DotNetCoreApiTemplate.Repositories.Interfaces;

namespace DotNetCoreApiTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var result = await _repo.GetCustomerOrdersAsync(customerId);
            return Ok(result);
        }
    }
}
