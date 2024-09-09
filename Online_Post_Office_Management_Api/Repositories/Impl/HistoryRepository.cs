using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly IMongoCollection<History> _historyCollection;

        public HistoryRepository(IMongoDatabase database)
        {
            _historyCollection = database.GetCollection<History>("History");
        }

        public async Task<IEnumerable<History>> GetAll()
        {
            return await _historyCollection.Find(_ => true).ToListAsync();
        }

        public async Task<History> SearchById(string id)
        {
            return await _historyCollection.Find(h => h.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(History history)
        {
            await _historyCollection.InsertOneAsync(history);
        }
    }
}
