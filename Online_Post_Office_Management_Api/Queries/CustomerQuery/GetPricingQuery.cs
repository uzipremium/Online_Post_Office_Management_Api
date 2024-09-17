using MediatR;

namespace Online_Post_Office_Management_Api.Queries.CustomerQuery
{
    public class GetPricingQuery : IRequest<decimal>
    {
        public string ServiceType { get; set; }
        public double Weight { get; set; }
        public double Distance { get; set; }

        public GetPricingQuery(string serviceType, double weight, double distance)
        {
            ServiceType = serviceType;
            Weight = weight;
            Distance = distance;
        }
    }
}
