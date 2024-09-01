using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly IMongoCollection<Delivery> _deliveries;

        public DeliveryRepository(IMongoDatabase database)
        {
            _deliveries = database.GetCollection<Delivery>("Deliveries");
        }

        public async Task<Delivery> GetById(string id)
        {
            return await _deliveries.Find(delivery => delivery.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Delivery>> GetAll()
        {
            return await _deliveries.Find(FilterDefinition<Delivery>.Empty).ToListAsync();
        }

        public async Task<bool> Update(string id, Delivery delivery)
        {
            var result = await _deliveries.ReplaceOneAsync(d => d.Id == id, delivery);
            return result.ModifiedCount > 0;
        }

    }
}
