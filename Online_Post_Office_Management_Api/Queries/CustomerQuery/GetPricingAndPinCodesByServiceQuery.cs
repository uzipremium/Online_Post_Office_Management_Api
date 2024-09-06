using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.CustomerQueries
{
    public class GetPricingAndPinCodesByServiceQuery : IRequest<(Service service, List<Office> offices)>
    {
        public string ServiceId { get; set; }
        public string OfficeId { get; set; }

        public GetPricingAndPinCodesByServiceQuery(string serviceId, string officeId)
        {
            ServiceId = serviceId;
            OfficeId = officeId;
        }
    }
}
