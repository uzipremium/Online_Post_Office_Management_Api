using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.IRepository
{
    public interface ICustomerSendHistoryRepository
    {
        Task<CustomerSendHistory> GetById(string id);               
        Task<IEnumerable<CustomerSendHistory>> GetAll();           
        Task Create(CustomerSendHistory history);                   
    }
}
