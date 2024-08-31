using MongoDB.Bson;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Data;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMongoCollection<Account> _accounts;

        public EmployeeRepository(MongoDbService mongoDbService)
        {
            _employees = mongoDbService.Database.GetCollection<Employee>("Employee");
            _accounts = mongoDbService.Database.GetCollection<Account>("Account");
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            return await _employees.Find(FilterDefinition<Employee>.Empty).ToListAsync();
        }

        public async Task<Employee> GetById(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            return await _employees.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Create(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Id))
            {
                employee.Id = ObjectId.GenerateNewId().ToString();
            }

            await _employees.InsertOneAsync(employee);
        }

        public async Task<bool> Update(string id, Employee updatedEmployee)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);

            var updateDefinition = Builders<Employee>.Update
                .Set(x => x.Email, updatedEmployee.Email)
                .Set(x => x.Phone, updatedEmployee.Phone)
                .Set(x => x.Gender, updatedEmployee.Gender)
                .Set(x => x.Name, updatedEmployee.Name)
                .Set(x => x.DateOfBirth, updatedEmployee.DateOfBirth)
                .Set(x => x.CreatedDate, updatedEmployee.CreatedDate)
                .Set(x => x.OfficeId, updatedEmployee.OfficeId)
                .Set(x => x.AccountId, updatedEmployee.AccountId);

            var updateResult = await _employees.UpdateOneAsync(filter, updateDefinition);

            return updateResult.MatchedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            var employee = await _employees.Find(filter).FirstOrDefaultAsync();

            if (employee == null)
            {
                return false;
            }

            var accountId = employee.AccountId;

            var deleteResult = await _employees.DeleteOneAsync(filter);

            if (deleteResult.DeletedCount > 0)
            {
                var accountFilter = Builders<Account>.Filter.Eq(x => x.Id, accountId);
                await _accounts.DeleteOneAsync(accountFilter);
                return true;
            }

            return false;
        }
    }
}
