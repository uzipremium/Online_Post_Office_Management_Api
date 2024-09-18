using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPaymentRepository
    {
<<<<<<< HEAD
        Task<Payment> GetById(string id);              
        Task<IEnumerable<Payment>> GetAll();     
        Task<bool> Update(string id, Payment payment);  
        Task<bool> Create(Payment payment);                          
=======
        Task<Payment> GetById(string id);
        Task<IEnumerable<Payment>> GetAll();
        Task<bool> Update(string id, Payment payment);
>>>>>>> 6184879bd73e25d12d9964bf5074c8e28e2afa0a
    }
}
