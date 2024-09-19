using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Queries.AccountQuery;
using Online_Post_Office_Management_Api.Commands.AccountCommand;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Handlers.AccountHandler;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(string id)
        {
            var query = new AccountGetOne(id, User);
            var account = await _mediator.Send(query);

            if (account == null)
            {
                return NotFound(new { message = "Account not found." });
            }

            return Ok(account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccountById(string id, [FromBody] UpdateAccount updateCommand)
        {
           
            updateCommand.Id = id;

          
            var result = await _mediator.Send(updateCommand);

            if (result == null)
            {
                return BadRequest(new { message = "Failed to update account." });
            }

            return Ok(new { message = "Account updated successfully." });
        }
    }
}
