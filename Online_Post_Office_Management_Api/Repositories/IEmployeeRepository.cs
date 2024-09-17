using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmployeeRepository
{
    Task<IEnumerable<EmployeeWithOfficeDto>> GetAll();
    Task<EmployeeWithOfficeDto> GetById(string id);
    Task Create(Employee employee);
    Task<EmployeeWithOfficeDto> Update(string id, UpdateEmployee request); 
    Task<bool> Delete(string id);
}
