using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;

namespace Online_Post_Office_Management_Api.Queries.CustomerQuery
{
    public class GetPincodeQuery : IRequest<PincodeResponse>
    {
        public string Location { get; set; }

        public GetPincodeQuery(string location)
        {
            Location = location;
        }
    }
}
