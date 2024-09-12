using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class CustomerSendHistoryRepository : ICustomerSendHistoryRepository
    {
        private readonly IMongoCollection<CustomerSendHistory> _customerSendHistories;

        public CustomerSendHistoryRepository(IMongoDatabase database)
        {
            _customerSendHistories = database.GetCollection<CustomerSendHistory>("CustomerSendHistories");
        }

        public async Task<CustomerSendHistory> GetById(string id)
        {
            return await _customerSendHistories.Find(history => history.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerSendHistory>> GetAll()
        {
            return await _customerSendHistories.Find(_ => true).ToListAsync();
        }

        public async Task Create(CustomerSendHistory history)
        {
            await _customerSendHistories.InsertOneAsync(history);
        }
    }
}
