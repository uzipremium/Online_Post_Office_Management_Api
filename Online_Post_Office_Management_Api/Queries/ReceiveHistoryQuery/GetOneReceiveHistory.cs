using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.ReceiveHistoryQuery
{
    public class GetOneReceiveHistory : IRequest<ReceiveHistory>
    {
        public string Id { get; set; }

        public GetOneReceiveHistory(string id)
        {
            Id = id;
        }
    }
}
