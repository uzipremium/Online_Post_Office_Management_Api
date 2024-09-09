using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetById(string id);
        Task<IEnumerable<Delivery>> GetAll();
        Task<bool> Update(string id, Delivery delivery);
        Task<bool> Create(Delivery delivery);
    }
}