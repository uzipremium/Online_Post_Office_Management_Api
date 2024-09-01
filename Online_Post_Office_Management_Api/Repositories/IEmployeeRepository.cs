using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task Create(Employee employee);
        Task<Employee> GetById(string id);
        Task<IEnumerable<Employee>> GetAll();
        Task<bool> Update(string id, Employee employee);
        Task<bool> Delete(string id);
    }
}
