using Online_Post_Office_Management_Api.Models;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using System.Threading.Tasks;
>>>>>>> f14bdc945562b2f8cf05f0b9b6217d8db97fc0b6

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPackageRepository
    {
<<<<<<< HEAD
        Task<Package> GetPackageByIdAsync(string packageId);
        Task<bool> UpdatePackageAsync(string packageId, Package package);
=======
        Task<IEnumerable<Package>> GetAllPackages();
        Task<Package> GetPackageById(string id); 
        Task CreatePackage(Package package); 
        Task<bool> UpdatePackage(string id, Package package); 
>>>>>>> f14bdc945562b2f8cf05f0b9b6217d8db97fc0b6
    }
}
