using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.CustomerSendHistoryQuery
{
    public class GetOneCustomerSendHistory : IRequest<CustomerSendHistory>
    {
        public string Id { get; set; }

        public GetOneCustomerSendHistory(string id)
        {
            Id = id;
        }
    }
}
