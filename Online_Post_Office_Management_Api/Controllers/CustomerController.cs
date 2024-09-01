using MediatR;
using Microsoft.AspNetCore.Mvc;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQuery;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("CheckPackageStatus")]
        public async Task<ActionResult<Package>> CheckPackageStatus([FromQuery] string phone, [FromQuery] string packageId)
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(packageId))
            {
                return BadRequest("Phone and Package ID are required.");
            }

            var query = new CheckPackageStatusQuery(phone, packageId);
            var package = await _mediator.Send(query);

            if (package == null)
            {
                return NotFound("Package not found or the phone number does not match the sender.");
            }

            return Ok(package);
        }
    }
}
