using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;

public interface IEmployeeRepository
{
    Task<IEnumerable<EmployeeWithOfficeDto>> GetAll();
    Task<EmployeeWithOfficeDto> GetById(string id);
    Task<EmployeeWithAccountWithOfficeDto> GetById2(string id);
    Task Create(Employee employee);
    Task<EmployeeWithOfficeDto> Update(string id, Employee employee); 
    Task<bool> Update2(string id, EmployeeWithAccountWithOfficeDto employee);
    Task<bool> Delete(string id);
}
