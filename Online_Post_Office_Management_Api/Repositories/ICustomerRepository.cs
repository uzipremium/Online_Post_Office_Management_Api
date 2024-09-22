using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.IRepository
{
    public interface ICustomerRepository
    {
        Task Create(Customer customer);  
        Task<IEnumerable<Customer>> GetAll(); 
        Task<Customer> FindByPhone(string phone);  
        Task<Customer> FindById(string id);   
    }
}
