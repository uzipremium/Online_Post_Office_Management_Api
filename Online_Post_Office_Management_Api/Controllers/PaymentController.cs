using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Commands.PaymentCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.PaymentQuery;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // Lấy payment theo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(string id)
        {
            try
            {
                var payment = await _mediator.Send(new PaymentGetOne(id));
                if (payment == null)
                {
                    return NotFound("Payment not found.");
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Cập nhật payment (chỉ dành cho admin và employee)
        [Authorize(Roles = "admin, employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePayment(string id, [FromBody] UpdatePayment command)
        {
            if (id != command.Id)
            {
                return BadRequest("Payment ID mismatch.");
            }

            try
            {
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok("Payment updated successfully.");
                }

                return NotFound("Payment not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Lấy tất cả các payment với phân trang và các điều kiện tùy chọn
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
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
