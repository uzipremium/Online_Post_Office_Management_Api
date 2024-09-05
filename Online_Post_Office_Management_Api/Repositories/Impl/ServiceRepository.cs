using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IMongoCollection<Service> _services;

        public ServiceRepository(IMongoDatabase database)
        {
            _services = database.GetCollection<Service>("Services");
        }

        public async Task<Service> GetById(string id)
        {
            return await _services.Find(service => service.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await _services.Find(_ => true).ToListAsync();
        }

        public async Task Create(Service service)
        {
            if (string.IsNullOrEmpty(service.Id))
            {
                service.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            }

            await _services.InsertOneAsync(service);
        }

        public async Task<bool> Update(string id, Service service)
        {
            var result = await _services.ReplaceOneAsync(s => s.Id == id, service);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _services.DeleteOneAsync(s => s.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
