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
        public async Task<ActionResult<PackageResponse>> CheckPackageStatus([FromQuery] string phone, [FromQuery] string packageId)
        {
            try
            {
                
                if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(packageId))
                {
                    return BadRequest(new { message = "Phone and Package ID are required." });
                }

            
                var query = new CheckPackageStatusQuery(phone, packageId);
                var packageResponse = await _mediator.Send(query);

             
                if (packageResponse == null)
                {
                    return NotFound(new { message = "Package not found." });
                }

                return Ok(packageResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Get Pricing
        [HttpGet("GetPricing")]
        public async Task<ActionResult<decimal>> GetPricing([FromQuery] string serviceId, [FromQuery] double weight, [FromQuery] double distance)
        {
            try
            {
                Console.WriteLine($"Service ID: {serviceId}, Weight: {weight}, Distance: {distance}");

                // Validate input
                if (string.IsNullOrEmpty(serviceId) || weight <= 0 || distance <= 0)
                {
                    return BadRequest(new { message = "Service ID, weight, and distance are required and must be valid values." });
                }

                // Send query to MediatR
                var query = new GetPricingQuery(serviceId, weight, distance);
                var price = await _mediator.Send(query);

                return Ok(price);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Get Pincode
        [HttpGet("GetPincode")]
        public async Task<ActionResult<PincodeResponse>> GetPincode([FromQuery] string officeId)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(officeId))
                {
                    return BadRequest(new { message = "Office ID is required." });
                }

                // Send query to MediatR
                var query = new GetPincodeQuery(officeId);
                var pincode = await _mediator.Send(query);

                // Check if pincode is null
                if (pincode == null)
                {
                    return NotFound(new { message = "Pincode not found for the specified office." });
                }

                return Ok(pincode);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
            
        }
    }
}
