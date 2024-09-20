using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Queries.AccountQuery;
using Online_Post_Office_Management_Api.Commands.AccountCommand;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Models;
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

        // GET: api/account/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById(string id)
        {
            try
            {
                var user = HttpContext.User; 

                var tokenString = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(tokenString);

                var query = new AccountGetOne(id, user, jwtToken);
                var account = await _mediator.Send(query);

                if (account == null)
                {
                    return NotFound(new { message = "Account not found." });
                }

                return Ok(account);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Return a 401 Unauthorized error when there is an authentication error
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error for unidentified errors
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        // PUT: api/account/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccountById(string id, [FromBody] UpdateAccount updateCommand)
        {
            try
            {
                // Assign the ID from the route to the command
                updateCommand.Id = id;

                // Send the command to update the account
                var result = await _mediator.Send(updateCommand);

                if (result == null)
                {
                    return BadRequest(new { message = "Account update failed." });
                }

                return Ok(new { message = "Account update successful." });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Return a 401 Unauthorized error when there is an authentication error
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error for unidentified errors
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }
    }
}
