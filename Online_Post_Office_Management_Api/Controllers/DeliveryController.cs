using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Commands.DeliveryCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.Deliveries;
using System.Threading.Tasks;

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

        // GET: api/Delivery/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDeliveryById(string id)
        {
            var delivery = await _mediator.Send(new DeliveryGetOne(id));
            if (delivery == null)
            {
                return NotFound("Delivery not found.");
            }
            return Ok(delivery);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDelivery(string id, [FromBody] UpdateDelivery command)
        {
            if (id != command.Id)
            {
                return BadRequest("Delivery ID mismatch.");
            }

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("Delivery updated successfully.");
            }

            return NotFound("Delivery not found.");
        }
    }
}
