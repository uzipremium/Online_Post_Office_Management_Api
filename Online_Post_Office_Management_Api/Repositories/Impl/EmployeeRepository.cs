using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employees;

        public EmployeeRepository(IMongoDatabase database)
        {
            _employees = database.GetCollection<Employee>("Employees");

        }

        public async Task Create(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
        }

        public async Task<Employee> GetById(string id)
        {
            return await _employees.Find(employee => employee.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employees.Find(FilterDefinition<Employee>.Empty).ToListAsync();
        }

        public async Task<bool> Update(string id, Employee updatedEmployee)
        {
            var existingEmployee = await _employees.Find(e => e.Id == id).FirstOrDefaultAsync();

            if (existingEmployee == null)
            {
                return false;
            }

            var result = await _employees.ReplaceOneAsync(e => e.Id == id, updatedEmployee);

            if (result.ModifiedCount > 0)
            {
       
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _employees.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }


    }
}