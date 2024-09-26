using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Queries.AccountQuery;
using Online_Post_Office_Management_Api.Commands.AccountCommand;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.DTO;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, employee")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
                    _logger.LogWarning("Authorization header is missing.");
                    return Unauthorized(new { message = "Authorization header is missing." });
                }

                var tokenString = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();

                // Kiểm tra và đọc token
                if (handler.ReadToken(tokenString) is not JwtSecurityToken jwtToken)
                {
                    _logger.LogWarning("Invalid token.");
                    return Unauthorized(new { message = "Invalid token." });
                }

                // Kiểm tra nếu token đã hết hạn
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    _logger.LogWarning("Token has expired.");
                    return Unauthorized(new { message = "Token has expired." });
                }

                // Lấy vai trò của người dùng
                var userRole = user.FindFirst(ClaimTypes.Role)?.Value;
                _logger.LogInformation($"User role: {userRole}");

                // Nếu người dùng không phải admin, kiểm tra ID có khớp không
                if (userRole != "admin")
                {
                    var userIdFromToken = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    _logger.LogInformation($"User ID from token: {userIdFromToken}");

                    if (userIdFromToken != id)
                    {
                        _logger.LogWarning("Invalid token: Account ID mismatch.");
                        return Unauthorized(new { message = "Invalid token: Account ID mismatch." });
                    }
                }
                else
                {
                    _logger.LogInformation("Admin role, bypassing Account ID check.");
                }

                // Gửi truy vấn để lấy thông tin tài khoản
                var query = new AccountWithEmployeeWithOfficeGetOne(id, user, jwtToken);
                var accountWithDetails = await _mediator.Send(query);

                if (accountWithDetails == null)
                {
                    _logger.LogWarning($"Account with ID {id} not found.");
                    return NotFound(new { message = "Account not found." });
                }

                return Ok(accountWithDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access.");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
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
                    _logger.LogWarning("Invalid request. Account details are missing.");
                    return BadRequest(new { message = "Invalid request. Please provide account details." });
                }

                updateCommand.AccountId = id;

                var result = await _mediator.Send(updateCommand);

                if (result == null)
                {
                    _logger.LogWarning($"Account or Employee not found for ID {id}.");
                    return NotFound(new { message = "Account or Employee not found." });
                }

                return Ok(new { message = "Account and Employee updated successfully.", data = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access.");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the account.");
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }
    }
}
