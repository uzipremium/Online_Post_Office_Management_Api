﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Commands.ServiceCommand;
using Online_Post_Office_Management_Api.Queries.ServiceQuery;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetService(string id)
        {
            try
            {
<<<<<<< HEAD
                return NotFound(new { message = "Service not found." });
            }
            return Ok(service);
=======
                var service = await _mediator.Send(new GetServiceQuery(id));
                if (service == null)
                {
                    return NotFound(new { message = "Service not found." });
                }
                return Ok(service);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                return StatusCode(500, "An error occurred while retrieving the service.");
            }
>>>>>>> dev2
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
<<<<<<< HEAD
            var services = await _mediator.Send(new GetAllServicesQuery());
            return Ok(services);
=======
            try
            {
                var services = await _mediator.Send(new GetAllServicesQuery());
                return Ok(services);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                return StatusCode(500, "An error occurred while retrieving services.");
            }
>>>>>>> dev2
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> CreateService([FromBody] Service service)
        {
            if (service == null)
            {
                return BadRequest(new { message = "Service data is required." });
            }

            try
            {
                await _mediator.Send(new CreateServiceCommand(service));
                return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                return StatusCode(500, "An error occurred while creating the service.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateService(string id, [FromBody] Service service)
        {
            if (id != service.Id)
            {
                return BadRequest(new { message = "Service ID mismatch." });
            }

            try
            {
<<<<<<< HEAD
                return Ok(new { message = "Service updated successfully." });
            }

            return NotFound(new { message = "Service not found." });
=======
                var result = await _mediator.Send(new UpdateServiceCommand(service));
                if (result)
                {
                    return Ok(new { message = "Service updated successfully." });
                }

                return NotFound(new { message = "Service not found." });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                return StatusCode(500, "An error occurred while updating the service.");
            }
>>>>>>> dev2
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(string id)
        {
            try
            {
<<<<<<< HEAD
                return Ok(new { message = "Service deleted successfully." });
            }

            return NotFound(new { message = "Service not found." });
=======
                var result = await _mediator.Send(new DeleteServiceCommand(id));
                if (result)
                {
                    return Ok(new { message = "Service deleted successfully." });
                }

                return NotFound(new { message = "Service not found." });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                return StatusCode(500, "An error occurred while deleting the service.");
            }
>>>>>>> dev2
        }
    }
}
