using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IDescriptionRepository
    {
<<<<<<< HEAD
        Task<Description> GetById(string id);                 
        Task<IEnumerable<Description>> GetAll();               
        Task<bool> Update(string id, Description description);  
        Task<bool> Create(Description description);        
   
=======
        Task<Description> GetById(string id);
        Task<IEnumerable<Description>> GetAll();
        Task<bool> Update(string id, Description description);
>>>>>>> 6184879bd73e25d12d9964bf5074c8e28e2afa0a
    }
}
