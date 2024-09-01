using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllPackages();
        Task<Package> GetPackageById(string id);
        Task CreatePackage(Package package);
        Task<bool> UpdatePackage(string id, Package package);
    }
}
