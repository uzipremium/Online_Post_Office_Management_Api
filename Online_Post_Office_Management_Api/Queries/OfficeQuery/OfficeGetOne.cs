using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.OfficeQuery
{
    public class OfficeGetOne : IRequest<Office>
    {
        public string Id { get; set; }

        public OfficeGetOne(string id)
        {
            Id = id;
        }
    }
}