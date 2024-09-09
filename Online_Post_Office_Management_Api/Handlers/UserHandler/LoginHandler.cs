using MediatR;
using Microsoft.IdentityModel.Tokens;
using Online_Post_Office_Management_Api.Commands.UserCommand;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.UserHandler
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly string _secretKey;

        public LoginHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _secretKey = configuration["Jwt:Key"];
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Login attempt for Username: {request.Username}");
            Console.WriteLine($"Login attempt for Password: {request.Password}");
            var (account, role) = await _userRepository.GetByUsernameAndPassword(request.Username, request.Password);

            if (account == null || role == null)
            {
                Console.WriteLine("Invalid credentials.");
                return null; // Trả về null nếu tài khoản hoặc vai trò không tồn tại
            }

            // Create JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Username),
                    new Claim(ClaimTypes.Role, role.RoleName) // Sử dụng RoleName từ Role
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginResponse
            {
                Username = account.Username,
                RoleName = role.RoleName, // Trả về RoleName từ Role
                Token = tokenString
            };
        }
    }
}
