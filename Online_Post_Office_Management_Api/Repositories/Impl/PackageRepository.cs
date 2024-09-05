using MongoDB.Bson;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class PackageRepository : IPackageRepository
    {
        private readonly IMongoCollection<Package> _packages;

        public PackageRepository(IMongoDatabase database)
        {
            _packages = database.GetCollection<Package>("Packages");
        }

        public async Task<Package> GetById(string id)
        {
            return await _packages.Find(package => package.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Package>> GetAll()
        {
            return await _packages.Find(_ => true).ToListAsync();
        }

        public async Task Create(Package package)
        {
            if (string.IsNullOrEmpty(package.Id))
            {
                package.Id = ObjectId.GenerateNewId().ToString();
            }

            await _packages.InsertOneAsync(package);
        }

        public async Task<bool> Update(string id, Package package)
        {
            var result = await _packages.ReplaceOneAsync(pkg => pkg.Id == id, package);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _packages.DeleteOneAsync(package => package.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
