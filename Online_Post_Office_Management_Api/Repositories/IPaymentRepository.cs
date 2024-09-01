using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetById(string id);                   
        Task<IEnumerable<Payment>> GetAll();                                
        Task<bool> Update(string id, Payment payment);      
                 
    }
}
