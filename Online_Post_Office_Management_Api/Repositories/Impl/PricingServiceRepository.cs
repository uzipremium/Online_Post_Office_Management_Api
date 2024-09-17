
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class PricingServiceRepository : IPricingServiceRepository
    {
        private readonly IMongoCollection<Service> _services;
        public PricingServiceRepository(IMongoDatabase database)
        {
            _services = database.GetCollection<Service>("Services");
        }
        public async Task<decimal> CalculateDeliveryPriceAsync(string serviceType, double weight, double distance)
        {
            var service = await _services.Find(s => s.Name == serviceType).FirstOrDefaultAsync();

            if (service == null)
            {
                throw new ArgumentException("Invalid service type provided.");
            }

            // Calculate the price based on base rate, weight, and distance
            decimal price = service.BaseRate + (service.RatePerKg * (decimal)weight) + (service.RatePerKm * (decimal)distance);

            return price;

        }
    }
}
