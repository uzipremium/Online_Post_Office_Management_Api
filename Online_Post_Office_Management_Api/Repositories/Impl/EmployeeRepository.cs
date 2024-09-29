using MongoDB.Driver;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMongoCollection<Office> _offices;
        private readonly IMongoCollection<Account> _accounts;

        public EmployeeRepository(IMongoDatabase database)
        {
            _employees = database.GetCollection<Employee>("Employees");
            _offices = database.GetCollection<Office>("Offices");
            _accounts = database.GetCollection<Account>("Accounts");
        }

        // Create a new employee
        public async Task Create(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
        }

        // Get employee information by Id
        public async Task<EmployeeWithOfficeDto> GetById(string id)
        {
            var employee = await _employees.Find(e => e.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                return null;
            }

            var office = await _offices.Find(o => o.Id == employee.OfficeId).FirstOrDefaultAsync();

            return new EmployeeWithOfficeDto
            {
                Id = employee.Id,
                Email = employee.Email,
                Phone = employee.Phone,
                Gender = employee.Gender,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                CreatedDate = employee.CreatedDate,
                OfficeId = employee.OfficeId,
                OfficeName = office?.OfficeName,
                AccountId = employee.AccountId
            };
        }

        // Get all employees with pagination
        public async Task<IEnumerable<EmployeeWithOfficeDto>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            int skip = (pageNumber - 1) * pageSize;

            var employees = await _employees.Find(FilterDefinition<Employee>.Empty)
                                            .SortByDescending(e => e.CreatedDate)
                                            .Skip(skip)
                                            .Limit(pageSize)
                                            .ToListAsync();

            var employeeDtos = new List<EmployeeWithOfficeDto>();

            foreach (var employee in employees)
            {
                var office = await _offices.Find(o => o.Id == employee.OfficeId).FirstOrDefaultAsync();

                employeeDtos.Add(new EmployeeWithOfficeDto
                {
                    Id = employee.Id,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Gender = employee.Gender,
                    Name = employee.Name,
                    DateOfBirth = employee.DateOfBirth,
                    CreatedDate = employee.CreatedDate,
                    OfficeId = employee.OfficeId,
                    OfficeName = office?.OfficeName,
                    AccountId = employee.AccountId
                });
            }

            return employeeDtos;
        }


        // Update employee by Id, return EmployeeWithOfficeDto
        public async Task<EmployeeWithOfficeDto> Update(string id, Employee employee)
        {
            var existingEmployee = await _employees.Find(e => e.Id == id).FirstOrDefaultAsync();

            if (existingEmployee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            var filter = Builders<Employee>.Filter.Eq(e => e.Id, id);
            var update = Builders<Employee>.Update
                .Set(e => e.Email, employee.Email)
                .Set(e => e.Phone, employee.Phone)
                .Set(e => e.Gender, employee.Gender)
                .Set(e => e.Name, employee.Name)
                .Set(e => e.DateOfBirth, employee.DateOfBirth)
                .Set(e => e.OfficeId, employee.OfficeId)
                .Set(e => e.AccountId, employee.AccountId)
                .Set(e => e.CreatedDate, employee.CreatedDate);

            var result = await _employees.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            if (result.ModifiedCount == 0)
            {
                throw new InvalidOperationException($"No updates were made to Employee with ID {id}");
            }

            return await GetById(id);
        }

        // Update Employee using EmployeeWithAccountWithOfficeDto, return bool
        public async Task<bool> Update2(string id, EmployeeWithAccountWithOfficeDto employeeWithAccountDto)
        {
            var filter = Builders<Employee>.Filter.Eq(e => e.Id, id);
            var update = Builders<Employee>.Update
                .Set(e => e.Email, employeeWithAccountDto.Email)
                .Set(e => e.Phone, employeeWithAccountDto.Phone)
                .Set(e => e.Gender, employeeWithAccountDto.Gender)
                .Set(e => e.Name, employeeWithAccountDto.Name)
                .Set(e => e.DateOfBirth, employeeWithAccountDto.DateOfBirth)
                .Set(e => e.OfficeId, employeeWithAccountDto.OfficeId)
                .Set(e => e.AccountId, employeeWithAccountDto.AccountId)
                .Set(e => e.CreatedDate, employeeWithAccountDto.CreatedDate);

            var result = await _employees.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        // Delete employee by Id
        public async Task<bool> Delete(string id)
        {
            var result = await _employees.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }

        // Get employee information by Id along with account information
        public async Task<EmployeeWithAccountWithOfficeDto> GetById2(string id)
        {
            var employee = await _employees.Find(e => e.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            var account = await _accounts.Find(a => a.Id == employee.AccountId).FirstOrDefaultAsync();
            var office = await _offices.Find(o => o.Id == employee.OfficeId).FirstOrDefaultAsync();

            if (account == null)
            {
                throw new KeyNotFoundException($"Account associated with Employee ID {id} not found.");
            }

            if (office == null)
            {
                throw new KeyNotFoundException($"Office associated with Employee ID {id} not found.");
            }

            return new EmployeeWithAccountWithOfficeDto
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                CreatedDate = employee.CreatedDate,
                Email = employee.Email,
                Phone = employee.Phone,
                OfficeId = employee.OfficeId,
                OfficeName = office.OfficeName,
                AccountId = employee.AccountId,
                Username = account.Username,
                RoleId = account.RoleId
            };
        }

        // Search employees with criteria and pagination
        public async Task<IEnumerable<EmployeeWithOfficeDto>> Search(string name = null, string officeId = null, string phone = null, string officeName = null, int pageNumber = 1, int pageSize = 10)
        {
            var filterBuilder = Builders<Employee>.Filter;
            var filter = FilterDefinition<Employee>.Empty;

            // Search by employee name (if any)
            if (!string.IsNullOrEmpty(name))
            {
                filter &= filterBuilder.Regex(e => e.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
            }

            // Search by OfficeId (if any)
            if (!string.IsNullOrEmpty(officeId))
            {
                filter &= filterBuilder.Eq(e => e.OfficeId, officeId);
            }

            // Search by Phone (if any)
            if (!string.IsNullOrEmpty(phone))
            {
                filter &= filterBuilder.Eq(e => e.Phone, phone);
            }

            // Search by office name (OfficeName)
            List<string> officeIds = null;
            if (!string.IsNullOrEmpty(officeName))
            {
                var offices = await _offices.Find(o => o.OfficeName.ToLower().Contains(officeName.ToLower())).ToListAsync();
                officeIds = offices.ConvertAll(o => o.Id);

                if (officeIds.Count > 0)
                {
                    filter &= filterBuilder.In(e => e.OfficeId, officeIds);
                }
                else
                {
                    return new List<EmployeeWithOfficeDto>();
                }
            }

            // Calculate the number of records to skip and limit the number of records per page
            int skip = (pageNumber - 1) * pageSize;

            // Get the list of employees based on the created filter and pagination
            var employees = await _employees.Find(filter)
                                   .SortByDescending(e => e.CreatedDate)
                                   .Skip(skip)
                                   .Limit(pageSize)
                                   .ToListAsync();

            var employeeDtos = new List<EmployeeWithOfficeDto>();

            foreach (var employee in employees)
            {
                var office = await _offices.Find(o => o.Id == employee.OfficeId).FirstOrDefaultAsync();
                employeeDtos.Add(new EmployeeWithOfficeDto
                {
                    Id = employee.Id,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Gender = employee.Gender,
                    Name = employee.Name,
                    DateOfBirth = employee.DateOfBirth,
                    CreatedDate = employee.CreatedDate,
                    OfficeId = employee.OfficeId,
                    OfficeName = office?.OfficeName,
                    AccountId = employee.AccountId
                });
            }

            return employeeDtos;
        }
    }
}
