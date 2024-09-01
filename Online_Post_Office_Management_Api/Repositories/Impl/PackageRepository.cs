using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class PackageRepository : IPackageRepository
    {
        private readonly IMongoCollection<Package> _packages;

        public PackageRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("qwer");
            _packages = database.GetCollection<Package>("Package");
        }

        public async Task<Package> GetPackageByIdAsync(string packageId)
        {
            return await _packages.Find(p => p.Id == packageId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePackageAsync(string packageId, Package package)
        {
            var updateResult = await _packages.ReplaceOneAsync(p => p.Id == packageId, package);
            return updateResult.ModifiedCount > 0;
        }
    }
}
}
