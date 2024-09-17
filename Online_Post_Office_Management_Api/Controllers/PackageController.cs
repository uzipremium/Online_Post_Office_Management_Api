using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Queries.PackageQuery;
using Online_Post_Office_Management_Api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
<<<<<<< HEAD
    [Authorize(Roles = "admin, employee")] 
=======
    //[Authorize(Roles = "Admin, Employee")]
>>>>>>> dev2
    public class PackageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Package
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetPackages([FromQuery] int pageNumber = 1, [FromQuery] string officeId = null, [FromQuery] DateTime? startDate = null, [FromQuery] string paymentStatus = null)
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


        // GET: api/Package/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(string id)
        {
            var package = await _mediator.Send(new GetPackageByIdQuery { Id = id });
            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }

        // POST: api/Package
        [HttpPost]
        public async Task<ActionResult<Package>> CreatePackage([FromBody] CreatePackage_Payment_Description_Delivery command)
        {
            if (command == null)
            {
                return BadRequest(new { Message = "The request body is required." });
            }

            var package = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPackageById), new { id = package.Id }, package);
        }

        // PUT: api/Package/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePackage(string id, [FromBody] UpdatePackage command)
        {
            if (id != command.Id)
            {
                return BadRequest("Package ID mismatch.");
            }

            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Package updated successfully.");
            }

            return NotFound("Package not found.");
        }
    }
}
