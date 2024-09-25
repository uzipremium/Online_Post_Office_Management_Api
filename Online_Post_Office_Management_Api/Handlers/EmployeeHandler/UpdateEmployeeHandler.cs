using MediatR;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
using Online_Post_Office_Management_Api.Data;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.EmployeeHandler
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, EmployeeWithOfficeDto>
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMongoCollection<Office> _offices;

        public UpdateEmployeeHandler(MongoDbService mongoDbService)
        {
            _employees = mongoDbService.Database.GetCollection<Employee>("Employees");
            _offices = mongoDbService.Database.GetCollection<Office>("Offices");
        }

        public async Task<EmployeeWithOfficeDto> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            // Check if the employee exists
            var existingEmployee = await _employees.Find(e => e.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
            }

            var filter = Builders<Employee>.Filter.Eq(e => e.Id, request.Id);
            var update = Builders<Employee>.Update
                .Set(e => e.Email, request.Email)
                .Set(e => e.Phone, request.Phone)
                .Set(e => e.Gender, request.Gender)
                .Set(e => e.Name, request.Name)
                .Set(e => e.DateOfBirth, request.DateOfBirth)
                .Set(e => e.OfficeId, request.OfficeId)
                .Set(e => e.AccountId, request.AccountId)
                .Set(e => e.CreatedDate, request.CreatedDate);

            var result = await _employees.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            if (result.ModifiedCount > 0)
            {
                var updatedEmployee = await _employees.Find(e => e.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                var office = await _offices.Find(o => o.Id == updatedEmployee.OfficeId).FirstOrDefaultAsync(cancellationToken);

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

            throw new Exception("Failed to update the employee details."); 
        }
    }
}
