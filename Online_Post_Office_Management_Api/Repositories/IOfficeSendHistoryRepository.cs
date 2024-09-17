using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.IRepository
{
    public interface IOfficeSendHistoryRepository
    {
        Task<OfficeSendHistory> GetById(string id);               
        Task<IEnumerable<OfficeSendHistory>> GetAll();            
        Task Create(OfficeSendHistory history);                 
    }
}
