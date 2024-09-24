using MongoDB.Driver;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMongoCollection<Office> _offices;

        public AccountRepository(IMongoDatabase database)
        {
            _accounts = database.GetCollection<Account>("Accounts");
            _employees = database.GetCollection<Employee>("Employees");
            _offices = database.GetCollection<Office>("Offices");
        }

        public async Task Create(Account account)
        {
            await _accounts.InsertOneAsync(account);
        }

        public async Task<Account?> GetById(string id)
        {
            return await _accounts.Find(account => account.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _accounts.Find(FilterDefinition<Account>.Empty).ToListAsync();
        }

        public async Task<EmployeeWithAccountWithOfficeDto?> GetAccountWithEmployeeAndOfficeById(string id)
        {
            var account = await _accounts.Find(a => a.Id == id).FirstOrDefaultAsync();
            if (account == null) return null;

            var employee = await _employees.Find(e => e.AccountId == account.Id).FirstOrDefaultAsync();
            if (employee == null) return null;

            var office = await _offices.Find(o => o.Id == employee.OfficeId).FirstOrDefaultAsync();

            return new EmployeeWithAccountWithOfficeDto
            {
                EmployeeId = employee.Id,
                Email = employee.Email,
                Phone = employee.Phone,
                Gender = employee.Gender,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                CreatedDate = employee.CreatedDate,
                OfficeId = employee.OfficeId,
                OfficeName = office?.OfficeName ?? "N/A",
                AccountId = account.Id,
                Username = account.Username,
                RoleId = account.RoleId
            };
        }

        public async Task<EmployeeWithAccountWithOfficeDto> Update(string id, EmployeeWithAccountWithOfficeDto accountDto)
        {
            // Update Account information
            var updateDefinition = Builders<Account>.Update
                .Set(a => a.Username, accountDto.Username)
                .Set(a => a.Password, accountDto.Password)
                .Set(a => a.RoleId, accountDto.RoleId); // Update fields based on accountDto

            var accountResult = await _accounts.UpdateOneAsync(a => a.Id == id, updateDefinition);

            if (accountResult.ModifiedCount == 0)
            {
                throw new KeyNotFoundException("Account update failed or account not found.");
            }

            // Find and update Employee data
            var employee = await _employees.Find(e => e.AccountId == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee associated with the account not found.");
            }

            var office = await _offices.Find(o => o.Id == employee.OfficeId).FirstOrDefaultAsync();

            return new EmployeeWithAccountWithOfficeDto
            {
                EmployeeId = employee.Id,
                Email = employee.Email,
                Phone = employee.Phone,
                Gender = employee.Gender,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                CreatedDate = employee.CreatedDate,
                OfficeId = employee.OfficeId,
                OfficeName = office?.OfficeName ?? "N/A",
                AccountId = id,
                Username = accountDto.Username,
                RoleId = accountDto.RoleId
            };
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _accounts.DeleteOneAsync(a => a.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
