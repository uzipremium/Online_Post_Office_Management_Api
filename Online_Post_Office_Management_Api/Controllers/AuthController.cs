using MediatR;
using Microsoft.AspNetCore.Mvc;
using Online_Post_Office_Management_Api.Commands.UserCommand;
using Online_Post_Office_Management_Api.DTO.Response;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);

            if (response == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(response);
        }
    }
}
