using MongoDB.Driver;
using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
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

        public EmployeeRepository(IMongoDatabase database)
        {
            _employees = database.GetCollection<Employee>("Employees");
            _offices = database.GetCollection<Office>("Offices");
        }

        public async Task Create(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
        }

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
                OfficeName = office?.OfficeName, // Đảm bảo lấy tên văn phòng
                AccountId = employee.AccountId
            };
        }

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
                    OfficeName = office?.OfficeName, // Đảm bảo lấy tên văn phòng
                    AccountId = employee.AccountId
                });
            }

            return employeeDtos;
        }

        public async Task<EmployeeWithOfficeDto> Update(string id, UpdateEmployee request)
        {
            var filter = Builders<Employee>.Filter.Eq(e => e.Id, id);
            var update = Builders<Employee>.Update
                .Set(e => e.Email, request.Email)
                .Set(e => e.Phone, request.Phone)
                .Set(e => e.Gender, request.Gender)
                .Set(e => e.Name, request.Name)
                .Set(e => e.DateOfBirth, request.DateOfBirth)
                .Set(e => e.OfficeId, request.OfficeId)
                 .Set(e => e.OfficeId, request.OfficeName)
                .Set(e => e.AccountId, request.AccountId)
                .Set(e => e.CreatedDate, request.CreatedDate);

            var result = await _employees.UpdateOneAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                // Lấy thông tin nhân viên đã cập nhật
                var updatedEmployee = await _employees.Find(e => e.Id == id).FirstOrDefaultAsync();
                var office = await _offices.Find(o => o.Id == updatedEmployee.OfficeId).FirstOrDefaultAsync();

                return new EmployeeWithOfficeDto
                {
                    Id = updatedEmployee.Id,
                    Email = updatedEmployee.Email,
                    Phone = updatedEmployee.Phone,
                    Gender = updatedEmployee.Gender,
                    Name = updatedEmployee.Name,
                    DateOfBirth = updatedEmployee.DateOfBirth,
                    CreatedDate = updatedEmployee.CreatedDate,
                    OfficeId = updatedEmployee.OfficeId,
                    OfficeName = office?.OfficeName, 
                    AccountId = updatedEmployee.AccountId
                };
            }

            return null; 
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _employees.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
