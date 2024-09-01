using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly IMongoCollection<Package> _packages;

        public PackageRepository(IMongoDatabase database)
        {
            _packages = database.GetCollection<Package>("Packages");
        }

        public async Task CreatePackage(Package package)
        {
            if (string.IsNullOrEmpty(package.Id))
            {
                package.Id = ObjectId.GenerateNewId().ToString();
            }

            await _packages.InsertOneAsync(package);
        }
        public async Task<IEnumerable<Package>> GetAllPackages()
        {
            return await _packages.Find(FilterDefinition<Package>.Empty).ToListAsync();
        }

        public async Task<Package> GetPackageById(string id)
        {
            var filter = Builders<Package>.Filter.Eq(p => p.Id, id);
            return await _packages.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePackage(string id, Package package)
        {
            var filter = Builders<Package>.Filter.Eq(p => p.Id, id);
            var updateResult = await _packages.ReplaceOneAsync(filter, package);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
