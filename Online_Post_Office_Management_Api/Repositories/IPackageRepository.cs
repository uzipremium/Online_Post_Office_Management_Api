using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPackageRepository
    {
        Task<Package> GetPackageById(string id);
        Task<PackageResponse> GetByPackageIdAndPhone(string packageId, string phone);

        Task<PackageResponse> GetById(string id);
        Task<IEnumerable<PackageResponse>> GetAll();
        Task Create(Package package);
        Task<bool> Update(string id, Package package);
        Task<bool> Delete(string id);
    }
}
