using MediatR;
using Microsoft.AspNetCore.Mvc;
using Online_Post_Office_Management_Api.DTO.Response;
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

        // Get Pricing
        [HttpGet("GetPricing")]
        public async Task<ActionResult<decimal>> GetPricing([FromQuery] string serviceType, [FromQuery] double weight, [FromQuery] double distance)
        {
            if (string.IsNullOrEmpty(serviceType) || weight <= 0 || distance <= 0)
            {
                return BadRequest("Service type, weight, and distance are required.");
            }

            var query = new GetPricingQuery(serviceType, weight, distance);
            var price = await _mediator.Send(query);

            if (price == 0)
            {
                return NotFound("Pricing information could not be retrieved.");
            }

            return Ok(price);
        }

        // Get Pincode
        [HttpGet("GetPincode")]
        public async Task<ActionResult<PincodeResponse>> GetPincode([FromQuery] string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return BadRequest("Location is required.");
            }

            var query = new GetPincodeQuery(location);
            var pincode = await _mediator.Send(query);

            if (pincode == null)
            {
                return NotFound("Pincode not found for the specified location.");
            }

            return Ok(pincode);
        }
    }
}
