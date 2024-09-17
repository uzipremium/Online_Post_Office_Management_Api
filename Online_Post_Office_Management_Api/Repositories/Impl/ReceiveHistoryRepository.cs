using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class ReceiveHistoryRepository : IReceiveHistoryRepository
    {
        private readonly IMongoCollection<ReceiveHistory> _receiveHistories;

        public ReceiveHistoryRepository(IMongoDatabase database)
        {
            _receiveHistories = database.GetCollection<ReceiveHistory>("ReceiveHistories");
        }

        public async Task<ReceiveHistory> GetById(string id)
        {
            return await _receiveHistories.Find(history => history.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ReceiveHistory>> GetAll()
        {
            return await _receiveHistories.Find(_ => true).ToListAsync();
        }

        public async Task Create(ReceiveHistory history)
        {
            await _receiveHistories.InsertOneAsync(history);
        }
    }
}
