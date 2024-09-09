using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly IHistoryRepository _historyRepository;

        public EmployeeRepository(IMongoDatabase database, IHistoryRepository historyRepository)
        {
            _employees = database.GetCollection<Employee>("Employees");
            _historyRepository = historyRepository;
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
                await LogHistory(existingEmployee, updatedEmployee, "CurrentUserId"); 
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _employees.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }

        private async Task LogHistory(Employee oldEmployee, Employee newEmployee, string currentUserId)
        {
            var changes = new List<History>();

            if (oldEmployee.Name != newEmployee.Name)
            {
                changes.Add(new History
                {
                    EmployeeId = newEmployee.Id,
                    FieldName = "Name",
                    OldValue = oldEmployee.Name,
                    NewValue = newEmployee.Name,
                    ChangeDate = DateTime.UtcNow,
                    ChangedBy = currentUserId
                });
            }

            if (oldEmployee.Email != newEmployee.Email)
            {
                changes.Add(new History
                {
                    EmployeeId = newEmployee.Id,
                    FieldName = "Email",
                    OldValue = oldEmployee.Email,
                    NewValue = newEmployee.Email,
                    ChangeDate = DateTime.UtcNow,
                    ChangedBy = currentUserId
                });
            }

            if (changes.Count > 0)
            {
                foreach (var history in changes)
                {
                    await _historyRepository.Create(history);
                }
            }
        }
    }
}
