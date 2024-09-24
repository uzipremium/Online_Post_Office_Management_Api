using MediatR;
using Online_Post_Office_Management_Api.DTO;
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;

namespace Online_Post_Office_Management_Api.Queries.AccountQuery
{
    public class AccountWithEmployeeWithOfficeGetOne : IRequest<EmployeeWithAccountWithOfficeDto>
    {
        public string Id { get; set; }

        public ClaimsPrincipal User { get; set; }

        public JwtSecurityToken Token { get; set; }

        public AccountWithEmployeeWithOfficeGetOne() { }

        public AccountWithEmployeeWithOfficeGetOne(string id, ClaimsPrincipal user, JwtSecurityToken token)
        {
            Id = id;
            User = user;
            Token = token;
        }
    }
}
