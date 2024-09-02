using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class CustomerPackageRepository : ICustomerPackageRepository
    {
        private readonly IMongoCollection<Package> _packages;

        public CustomerPackageRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("qwer");
            _packages = database.GetCollection<Package>("Packages");
        }

        public async Task<Package> GetPackageByIdAsync(string packageId)
        {
            return await _packages.Find(p => p.Id == packageId).FirstOrDefaultAsync();
        }

  
    }
}

