using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPackageRepository
    {
        Task<Package> GetPackageByIdAsync(string packageId);
        Task<bool> UpdatePackageAsync(string packageId, Package package);
    }
}
