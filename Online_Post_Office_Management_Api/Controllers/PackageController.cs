using MediatR;
using Microsoft.AspNetCore.Mvc;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Queries.PackageQuery;
using Online_Post_Office_Management_Api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Package
        [HttpGet]
        public async Task<ActionResult<List<Package>>> GetAllPackages()
        {
            var packages = await _mediator.Send(new PackageGetAll());
            return Ok(packages);
        }

        // GET: api/Package/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackageById(string id)
        {
            var package = await _mediator.Send(new PackageGetOne(id));
            return package is not null ? Ok(package) : NotFound();
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
