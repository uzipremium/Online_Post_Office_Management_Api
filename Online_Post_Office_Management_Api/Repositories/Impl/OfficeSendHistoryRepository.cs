using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class OfficeSendHistoryRepository : IOfficeSendHistoryRepository
    {
        private readonly IMongoCollection<OfficeSendHistory> _officeSendHistories;

        public OfficeSendHistoryRepository(IMongoDatabase database)
        {
            _officeSendHistories = database.GetCollection<OfficeSendHistory>("OfficeSendHistories");
        }

        public async Task<OfficeSendHistory> GetById(string id)
        {
            return await _officeSendHistories.Find(history => history.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OfficeSendHistory>> GetAll()
        {
            return await _officeSendHistories.Find(_ => true).ToListAsync();
        }

        public async Task Create(OfficeSendHistory history)
        {
            await _officeSendHistories.InsertOneAsync(history);
        }
    }
}
