using MediatR;
using Microsoft.IdentityModel.Tokens;
using Online_Post_Office_Management_Api.Commands.UserCommand;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Online_Post_Office_Management_Api.Handlers.UserHandler
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly string _secretKey;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(IUserRepository userRepository, IConfiguration configuration, ILogger<LoginHandler> logger)
        {
            _userRepository = userRepository;
            _secretKey = configuration["Jwt:Key"];
            _logger = logger;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {


            _logger.LogInformation($"Login attempt for Username: {request.Username}");

            // Lấy account theo Username, không cần mật khẩu vì mật khẩu được mã hóa
            var (account, role) = await _userRepository.GetByUsername(request.Username);

            if (account == null || role == null)
            {
                _logger.LogWarning("Invalid credentials: Username not found.");
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // Kiểm tra mật khẩu với BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
            if (!isPasswordValid)
            {
                _logger.LogWarning("Invalid credentials: Password mismatch.");
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // Tạo JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id),
                    new Claim(ClaimTypes.Name, account.Username),
                    new Claim(ClaimTypes.Role, role.RoleName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            _logger.LogInformation($"Login successful for Username: {request.Username}");

            return new LoginResponse
            {
                Id = account.Id,
                Username = account.Username,
                RoleName = role.RoleName,
                Token = tokenString
            };
        }
    }
}
