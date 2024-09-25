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

        // Tạo mới nhân viên
        public async Task Create(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
        }

        // Lấy thông tin nhân viên theo Id
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

        // Lấy tất cả nhân viên
        public async Task<IEnumerable<EmployeeWithOfficeDto>> GetAll()
        {
            var employees = await _employees.Find(FilterDefinition<Employee>.Empty).ToListAsync();
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

        // Cập nhật nhân viên theo Id, trả về EmployeeWithOfficeDto
        public async Task<EmployeeWithOfficeDto> Update(string id, Employee employee)
        {
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

            if (result.ModifiedCount > 0)
            {
                return await GetById(id);
            }

            return null;
        }

        // Cập nhật Employee với EmployeeWithAccountWithOfficeDto, trả về bool
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

        // Xóa nhân viên theo Id
        public async Task<bool> Delete(string id)
        {
            var result = await _employees.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }

        // Lấy thông tin nhân viên theo Id và thông tin tài khoản
        public async Task<EmployeeWithAccountWithOfficeDto> GetById2(string id)
        {
            var employee = await _employees.Find(e => e.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                return null;
            }

            var account = await _accounts.Find(a => a.Id == employee.AccountId).FirstOrDefaultAsync();
            var office = await _offices.Find(o => o.Id == employee.OfficeId).FirstOrDefaultAsync();

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
                OfficeName = office?.OfficeName,
                AccountId = employee.AccountId,
                Username = account?.Username,
                RoleId = account?.RoleId
            };
        }

   
    }
}
