using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.OfficeSendHistoryQuery
{
    public class GetOneOfficeSendHistory : IRequest<OfficeSendHistory>
    {
        public string Id { get; set; }

        // Constructor
        public GetOneOfficeSendHistory(string id)
        {
            Id = id;
        }
    }
}
