using MediatR;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQuery;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class GetPricingQueryHandler : IRequestHandler<GetPricingQuery, decimal>
    {
        private readonly IMongoCollection<Service> _services;

        public GetPricingQueryHandler(IMongoDatabase database)
        {
            _services = database.GetCollection<Service>("Services");
        }

        public async Task<decimal> Handle(GetPricingQuery request, CancellationToken cancellationToken)
        {
            var service = await _services.Find(s => s.Id == request.ServiceId).FirstOrDefaultAsync(cancellationToken);

            if (service == null)
            {
                throw new ArgumentException("Invalid service ID provided.");
            }

            decimal price = service.BaseRate +
                            (service.RatePerKg * (decimal)request.Weight) +
                            (service.RatePerKm * (decimal)request.Distance);

            return price;
        }
    }
}
