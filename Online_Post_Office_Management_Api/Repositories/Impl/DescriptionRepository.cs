using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class DescriptionRepository : IDescriptionRepository
    {
        private readonly IMongoCollection<Description> _descriptions;

        public DescriptionRepository(IMongoDatabase database)
        {
            _descriptions = database.GetCollection<Description>("Descriptions");
        }

        public async Task<Description> GetById(string id)
        {
            return await _descriptions.Find(description => description.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Description>> GetAll()
        {
            return await _descriptions.Find(FilterDefinition<Description>.Empty).ToListAsync();
        }

        public async Task<bool> Update(string id, Description description)
        {
            var result = await _descriptions.ReplaceOneAsync(d => d.Id == id, description);
            return result.ModifiedCount > 0;
        }
    }
}
