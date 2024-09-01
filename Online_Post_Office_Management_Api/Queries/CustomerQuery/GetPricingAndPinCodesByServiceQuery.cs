using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.CustomerQueries
{
    public class GetPricingAndPinCodesByServiceQuery : IRequest<(Service service, List<Office> offices)>
    {
        public string ServiceName { get; set; }

        public GetPricingAndPinCodesByServiceQuery(string serviceName)
        {
            ServiceName = serviceName;
        }
    }
}
