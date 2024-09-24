using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Queries.AccountQuery;
using Online_Post_Office_Management_Api.Commands.AccountCommand;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.DTO;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, employee")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWithAccountWithOfficeDto>> GetAccountById(string id)
        {
            try
            {
                var user = HttpContext.User;

                // Lấy token từ Header
                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    return Unauthorized(new { message = "Authorization header is missing." });
                }

                var tokenString = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();

                // Kiểm tra và đọc token
                if (handler.ReadToken(tokenString) is not JwtSecurityToken jwtToken)
                {
                    return Unauthorized(new { message = "Invalid token." });
                }

                var query = new AccountWithEmployeeWithOfficeGetOne(id, user, jwtToken);
                var accountWithDetails = await _mediator.Send(query);

                if (accountWithDetails == null)
                {
                    return NotFound(new { message = "Account not found." });
                }

                return Ok(accountWithDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccountById(string id, [FromBody] UpdateAccount_Employee updateCommand)
        {
            try
            {
                if (updateCommand == null)
                {
                    return BadRequest(new { message = "Invalid request. Please provide account details." });
                }

                updateCommand.AccountId = id;

                var result = await _mediator.Send(updateCommand);

                if (result == null)
                {
                    return NotFound(new { message = "Account or Employee not found." });
                }

                return Ok(new { message = "Account and Employee updated successfully.", data = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }
    }
}
