using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IServiceRepository
    {
        Task<Service> GetById(string id);
        Task<IEnumerable<Service>> GetAll();
        Task Create(Service service);
        Task<bool> Update(string id, Service service);
        Task<bool> Delete(string id);
    }
}
