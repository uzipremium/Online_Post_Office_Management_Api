using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IHistoryRepository
    {
        Task<IEnumerable<History>> GetAll();
        Task<History> SearchById(string id);
        Task Create(History history);
    }
}
