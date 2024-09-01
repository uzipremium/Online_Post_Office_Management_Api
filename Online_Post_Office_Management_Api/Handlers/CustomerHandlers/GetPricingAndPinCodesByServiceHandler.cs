using MediatR;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQueries;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class GetPricingAndPinCodesByServiceHandler : IRequestHandler<GetPricingAndPinCodesByServiceQuery, (Service, List<Office>)>
    {
        private readonly IMongoCollection<Service> _services;
        private readonly IMongoCollection<Office> _offices;

        public GetPricingAndPinCodesByServiceHandler(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("qwer");
            _services = database.GetCollection<Service>("Service");
            _offices = database.GetCollection<Office>("Office");
        }

        public async Task<(Service, List<Office>)> Handle(GetPricingAndPinCodesByServiceQuery request, CancellationToken cancellationToken)
        {
            var service = await _services.Find(s => s.Name.ToString() == request.ServiceName).FirstOrDefaultAsync(cancellationToken);

            if (service == null)
            {
                return (null, null);
            }

            var offices = await _offices.Find(FilterDefinition<Office>.Empty).ToListAsync(cancellationToken);
            
            return (service, offices);

        }
    }
}
