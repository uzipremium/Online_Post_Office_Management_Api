using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands.AccountCommand
{
    public class UpdateAccount : IRequest<Account>
    {
        public string Id { get; set; }  
        public string Username { get; set; }  
        public string Password { get; set; }  
        public string RoleId { get; set; } 
        public UpdateAccount() { }

        public UpdateAccount(string id, string username, string password, string roleId)
        {
            Id = id;
            Username = username;
            Password = password;
            RoleId = roleId;
        }
    }
}
