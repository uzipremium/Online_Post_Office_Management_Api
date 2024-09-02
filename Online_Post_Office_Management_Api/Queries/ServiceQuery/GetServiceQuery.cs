using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.ServiceQuery
{
    public class GetServiceQuery : IRequest<Service>
    {
        public string Id { get; set; }

        public GetServiceQuery(string id)
        {
            Id = id;
        }
    }
}
