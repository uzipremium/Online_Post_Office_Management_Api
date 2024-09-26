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
            try
            {
                // Kiểm tra nếu nhân viên tồn tại
                var existingEmployee = await _employees.Find(e => e.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if (existingEmployee == null)
                {
                    throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
                }

                // Xây dựng lệnh cập nhật
                var filter = Builders<Employee>.Filter.Eq(e => e.Id, request.Id);
                var update = Builders<Employee>.Update
                    .Set(e => e.Email, request.Email)
                    .Set(e => e.Phone, request.Phone)
                    .Set(e => e.Gender, request.Gender)
                    .Set(e => e.Name, request.Name)
                    .Set(e => e.DateOfBirth, request.DateOfBirth)
                    .Set(e => e.OfficeId, request.OfficeId)
                    .Set(e => e.CreatedDate, request.CreatedDate);

                // Chỉ cập nhật `AccountId` nếu không rỗng hoặc null
                if (!string.IsNullOrEmpty(request.AccountId))
                {
                    update = update.Set(e => e.AccountId, request.AccountId);
                }

                var result = await _employees.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

                // Kiểm tra xem có tài liệu nào được chỉnh sửa không
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
                else
                {
                    throw new Exception("No document was modified.");
                }
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết
                Console.WriteLine($"Error updating employee: {ex.Message}");
                throw new Exception("Failed to update the employee details.", ex);
            }
        }
    }
}
