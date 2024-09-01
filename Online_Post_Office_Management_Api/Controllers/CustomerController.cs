using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Post_Office_Management_Api.Commands.CustomerCommands;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQueries;

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

        [HttpGet("check-status")]
        public async Task<ActionResult<Package>> CheckPackageStatus([FromQuery] string phone, [FromQuery] string packageId)
        {
            var query = new CheckPackageStatusQuery(phone, packageId);
            var package = await _mediator.Send(query);

            if (package == null)
            {
                return NotFound(new { Message = "Package not found or phone number does not match." });
            }

            return Ok(package);
        }

        [HttpGet("pricing-and-pincodes")]
        public async Task<ActionResult> GetPricingAndPinCodesByService([FromQuery] string serviceName)
        {
            var query = new GetPricingAndPinCodesByServiceQuery(serviceName);
            var (service, offices) = await _mediator.Send(query);

            if (service == null)
            {
                return NotFound(new { Message = "Service not found." });
            }

            return Ok(new
            {
                Service = service,
                PinCodes = offices
            });
        }

        [HttpPost("make-payment")]
        public async Task<ActionResult> MakePayment([FromQuery] string packageId)
        {
            // Create the command to initiate the payment process
            var command = new MakePaymentCommand(packageId);

            // Send the command to the MakePaymentHandler
            var result = await _mediator.Send(command);

            if (!result)
            {
                // Return a bad request if payment fails or if it's not required (e.g., for VPP)
                return BadRequest(new { Message = "Payment failed or not required for VPP service." });
            }

            // Return a success response if payment was successful
            return Ok(new { Message = "Payment successful." });
        }
    }
}
