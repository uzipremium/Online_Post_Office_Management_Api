using MediatR;

namespace Online_Post_Office_Management_Api.Queries.CustomerQuery
{
    public class GetPricingQuery : IRequest<decimal>
    {
        public string ServiceId { get; set; }
        public double Weight { get; set; }
        public double Distance { get; set; }

        public GetPricingQuery(string serviceId, double weight, double distance)
        {
            ServiceId = serviceId;
            Weight = weight;
            Distance = distance;
        }
    }
}
