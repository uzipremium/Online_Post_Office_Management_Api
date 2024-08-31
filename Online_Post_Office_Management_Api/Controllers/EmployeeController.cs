using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Data;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMongoCollection<Account> _accounts;
        private readonly IMongoCollection<Office> _offices;
        private readonly IMongoCollection<Role> _roles;

        public EmployeeController(MongoDbService mongoDbService)
        {
            _employees = mongoDbService.Database.GetCollection<Employee>("Employee");
            _accounts = mongoDbService.Database.GetCollection<Account>("Account");
            _offices = mongoDbService.Database.GetCollection<Office>("Office");
            _roles = mongoDbService.Database.GetCollection<Role>("Role");
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _employees.Find(FilterDefinition<Employee>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee?>> GetById(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            var employee = await _employees.Find(filter).FirstOrDefaultAsync();
            return employee is not null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployeeWithAccount([FromBody] EmployeeWithAccountDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { Message = "The dto field is required." });
            }

            var employee = dto.Employee;
            var account = dto.Account;

            if (string.IsNullOrEmpty(account.Id))
            {
                account.Id = ObjectId.GenerateNewId().ToString();
            }

            if (string.IsNullOrEmpty(employee.Id))
            {
                employee.Id = ObjectId.GenerateNewId().ToString();
            }

            employee.AccountId = account.Id;

            var officeExists = await _offices.Find(x => x.Id == employee.OfficeId).AnyAsync();
            var roleExists = await _roles.Find(x => x.Id == account.RoleId).AnyAsync();

            if (!officeExists)
            {
                return BadRequest(new { Message = "The specified OfficeId does not exist." });
            }

            if (!roleExists)
            {
                return BadRequest(new { Message = "The specified RoleId does not exist." });
            }

            await _accounts.InsertOneAsync(account);

            employee.CreatedDate = DateTime.UtcNow;
            await _employees.InsertOneAsync(employee);

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(string id, [FromBody] Employee updatedEmployee)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);

            var employee = await _employees.Find(filter).FirstOrDefaultAsync();
            if (employee is null)
            {
                return NotFound();
            }

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

            if (updateResult.MatchedCount > 0)
            {
                return Ok();
            }

            return BadRequest("Update failed.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var employeeFilter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            var employee = await _employees.Find(employeeFilter).FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            var accountId = employee.AccountId;

            var deleteResult = await _employees.DeleteOneAsync(employeeFilter);

            if (deleteResult.DeletedCount > 0)
            {
                var accountFilter = Builders<Account>.Filter.Eq(x => x.Id, accountId);
                await _accounts.DeleteOneAsync(accountFilter);

                return Ok();
            }

            return BadRequest("Failed to delete the Employee.");
        }
    }
}
