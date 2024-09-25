using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IAccountRepository
    {
        Task Create(Account account);

        Task<Account> GetById(string id);
        Task<Account> GetByUsername(string name);

        Task<IEnumerable<Account>> GetAll();

        Task<EmployeeWithAccountWithOfficeDto?> GetAccountWithEmployeeAndOfficeById(string id);

        Task<EmployeeWithAccountWithOfficeDto> Update(string id, EmployeeWithAccountWithOfficeDto account);
        Task<bool> Delete(string id);
    }
}
