using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IAccountRepository
    {
        Task Create(Account account);
        Task<Account> GetById(string id); 
        Task<IEnumerable<Account>> GetAll();
        Task<bool> Update(string id, Account account); 
        Task<bool> Delete(string id);
    }
}
