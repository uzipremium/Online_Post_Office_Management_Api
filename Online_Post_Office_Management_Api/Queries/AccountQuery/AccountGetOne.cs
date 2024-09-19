using MediatR;
using Online_Post_Office_Management_Api.Models;
using System.Security.Claims;

namespace Online_Post_Office_Management_Api.Queries.AccountQuery
{
    public class AccountGetOne : IRequest<Account>
    {
        public string Id { get; set; }
        public ClaimsPrincipal User { get; set; } 

        public AccountGetOne() { }

        public AccountGetOne(string id, ClaimsPrincipal user)
        {
            Id = id;
            User = user;
        }
    }
}
