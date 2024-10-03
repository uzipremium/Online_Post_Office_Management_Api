using MediatR;
using Microsoft.AspNetCore.Mvc;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Queries.PackageQuery;
using Online_Post_Office_Management_Api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Exceptions;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, employee")]

    public class PackageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Package
        [HttpGet]
        public async Task<IActionResult> GetPackages([FromQuery] int pageNumber = 1, [FromQuery] string officeId = null, [FromQuery] DateTime? startDate = null, [FromQuery] string paymentStatus = null)
        {
            try
            {
                var query = new GetAllPackagesQuery
                {
                    PageNumber = pageNumber,
                    OfficeId = officeId,
                    StartDate = startDate,
                    PaymentStatus = paymentStatus
                };

                var packages = await _mediator.Send(query);
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        // GET: api/Package/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(string id)
        {
            try
            {
                var package = await _mediator.Send(new GetPackageByIdQuery { Id = id });
                if (package == null)
                {
                    return NotFound(new { message = "Package not found." });
                }

                return Ok(package);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        // POST: api/Package
        [HttpPost]
        public async Task<ActionResult<Package>> CreatePackage([FromBody] CreatePackage_Payment_Description_Delivery_Customer command)
        {
            if (command == null)
            {
                return BadRequest(new { Message = "The request body is required." });
            }

            try
            {
                var package = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetPackageById), new { id = package.Id }, package);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        // PUT: api/Package/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePackage(string id, [FromBody] UpdatePackage command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { message = "Package ID mismatch." });
            }

            try
            {
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok(new { message = "Package updated successfully." });
                }

                return NotFound(new { message = "Package not found." });
            }
            catch (NoChangeException ex)
            {
                return Ok(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }
    }
}
