using MongoDB.Bson;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Data;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountRepository(MongoDbService mongoDbService)
        {
            _accounts = mongoDbService.Database.GetCollection<Account>("Account");
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _accounts.Find(FilterDefinition<Account>.Empty).ToListAsync();
        }

        public async Task<Account> GetById(string id)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.Id, id);
            return await _accounts.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Create(Account account)
        {
            if (string.IsNullOrEmpty(account.Id))
            {
                account.Id = ObjectId.GenerateNewId().ToString();
            }

            await _accounts.InsertOneAsync(account);
        }

        public async Task<bool> Update(string id, Account updatedAccount)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.Id, id);

            var updateDefinition = Builders<Account>.Update
                .Set(x => x.Username, updatedAccount.Username)
                .Set(x => x.Password, updatedAccount.Password)
                .Set(x => x.RoleId, updatedAccount.RoleId);

            var updateResult = await _accounts.UpdateOneAsync(filter, updateDefinition);

            return updateResult.MatchedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.Id, id);
            var deleteResult = await _accounts.DeleteOneAsync(filter);

            return deleteResult.DeletedCount > 0;
        }
    }
}
