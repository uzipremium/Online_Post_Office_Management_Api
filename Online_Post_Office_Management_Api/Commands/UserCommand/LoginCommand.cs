using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;

namespace Online_Post_Office_Management_Api.Commands.UserCommand
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
