using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Commands.PaymentCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.PaymentQuery;
using System.Collections.Generic;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Exceptions;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get payment by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(string id)
        {
            try
            {
                var payment = await _mediator.Send(new PaymentGetOne(id));
                if (payment == null)
                {
                    return NotFound(new { message = "Payment not found." });
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        // Update payment (only for admin and employee)
        [Authorize(Roles = "admin, employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePayment(string id, [FromBody] UpdatePayment command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { message = "Payment ID mismatch." });
            }

            try
            {
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok(new { message = "Payment updated successfully." });
                }

                return NotFound(new { message = "Payment not found." });
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

        // Get all payments with pagination and optional conditions
        [Authorize(Roles = "admin, employee")]
        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllPayments([FromQuery] PaymentGetAll query)
        {
            try
            {
                var payments = await _mediator.Send(query);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }
    }
}
