using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.IRepository
{
    public interface IReceiveHistoryRepository
    {
        Task<ReceiveHistory> GetById(string id);
        Task<IEnumerable<ReceiveHistory>> GetAll();
        Task Create(ReceiveHistory history);
    }
}
