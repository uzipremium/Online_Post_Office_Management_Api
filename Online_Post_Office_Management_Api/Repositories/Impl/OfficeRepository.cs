using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly IMongoCollection<Office> _offices;

        public OfficeRepository(IMongoDatabase database)
        {
            _offices = database.GetCollection<Office>("Offices");
        }

        public async Task<Office> GetById(string id)
        {
            return await _offices.Find(office => office.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Office>> GetAll()
        {
            return await _offices.Find(_ => true).ToListAsync();
        }

        public async Task Create(Office office)
        {
            if (string.IsNullOrEmpty(office.Id))
            {
                office.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            }

            await _offices.InsertOneAsync(office);
        }

        public async Task<bool> Update(string id, Office office)
        {
            var result = await _offices.ReplaceOneAsync(o => o.Id == id, office);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _offices.DeleteOneAsync(o => o.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
