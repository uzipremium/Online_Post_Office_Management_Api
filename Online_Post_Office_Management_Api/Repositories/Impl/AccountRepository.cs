using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountRepository(IMongoDatabase database)
        {
            _accounts = database.GetCollection<Account>("Accounts");
        }

        public async Task Create(Account account)
        {
            await _accounts.InsertOneAsync(account);
        }

        public async Task<Account> GetById(string id)
        {
            return await _accounts.Find(account => account.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _accounts.Find(FilterDefinition<Account>.Empty).ToListAsync();
        }

        public async Task<bool> Update(string id, Account account)
        {
            var result = await _accounts.ReplaceOneAsync(a => a.Id == id, account);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _accounts.DeleteOneAsync(a => a.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
