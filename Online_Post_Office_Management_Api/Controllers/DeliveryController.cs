using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Commands.DeliveryCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.Deliveries;
using System;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Exceptions;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDeliveryById(string id)
        {
            try
            {
                var delivery = await _mediator.Send(new DeliveryGetOne(id));
                if (delivery == null)
                {
                    return NotFound(new { message = "Delivery not found." });
                }
                return Ok(delivery);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDelivery(string id, [FromBody] UpdateDelivery command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { message = "Delivery ID mismatch." });
            }

            try
            {
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok(new { message = "Delivery updated successfully." });
                }

                return NotFound("Delivery not found.");
            }
            catch (NoChangeException ex)
            {
                return Ok(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
