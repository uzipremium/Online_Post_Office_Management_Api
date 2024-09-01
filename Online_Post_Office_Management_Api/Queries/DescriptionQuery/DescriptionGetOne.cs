using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.DescriptionQuery
{
    public class DescriptionGetOne : IRequest<Description>
    {
        public string Id { get; set; }

        public DescriptionGetOne(string id)
        {
            Id = id;
        }
    }
}
