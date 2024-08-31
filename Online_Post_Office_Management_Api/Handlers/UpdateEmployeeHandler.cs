using MediatR;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Commands;
using Online_Post_Office_Management_Api.Data;

namespace Online_Post_Office_Management_Api.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, int>
    {
        private readonly IMongoCollection<Employee> _employees;

        public UpdateEmployeeHandler(MongoDbService mongoDbService)
        {
            _employees = mongoDbService.Database.GetCollection<Employee>("Employee");
        }

        public async Task<int> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            var filter = Builders<Employee>.Filter.Eq(e => e.Id, request.Id);

            var update = Builders<Employee>.Update
                .Set(e => e.Email, request.Email)
                .Set(e => e.Phone, request.Phone)
                .Set(e => e.Gender, request.Gender)
                .Set(e => e.Name, request.Name)
                .Set(e => e.DateOfBirth, request.DateOfBirth)
                .Set(e => e.OfficeId, request.OfficeId);

            var result = await _employees.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            return (int)result.ModifiedCount; 
        }
    }
}
