using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;

public interface IEmployeeRepository
{
    // GetAll với phân trang mặc định là 10 bản ghi mỗi trang
    Task<IEnumerable<EmployeeWithOfficeDto>> GetAll(int pageNumber = 1, int pageSize = 10);

    Task<EmployeeWithOfficeDto> GetById(string id);
    Task<EmployeeWithAccountWithOfficeDto> GetById2(string id);
    Task Create(Employee employee);
    Task<EmployeeWithOfficeDto> Update(string id, Employee employee);
    Task<bool> Update2(string id, EmployeeWithAccountWithOfficeDto employee);
    Task<bool> Delete(string id);

    Task<IEnumerable<EmployeeWithOfficeDto>> Search(string name = null, string officeId = null, string phone = null, string officeName = null, int pageNumber = 1, int pageSize = 10);
}
