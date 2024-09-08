using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.IRepository
{
    public interface IHistoryRepository
    {
        Task<IEnumerable<History>> GetAll();
        Task<History> SearchById(string id);
        Task Create(History history);
    }
}
