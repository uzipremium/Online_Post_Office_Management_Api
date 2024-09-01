using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.RoleQuery
{
    public class RoleGetOne : IRequest<Role>
    {
        public string Id { get; set; }

        public RoleGetOne(string id)
        {
            Id = id;
        }
    }
}
