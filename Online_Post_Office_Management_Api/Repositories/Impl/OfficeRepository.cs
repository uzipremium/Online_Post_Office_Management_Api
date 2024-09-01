using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly IMongoCollection<Office> _officeCollection;

        public OfficeRepository(IMongoDatabase database)
        {
            _officeCollection = database.GetCollection<Office>("Offices");
        }

        public async Task<Office> GetById(string id)
        {
            return await _officeCollection.Find(office => office.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Office>> GetAll()
        {
            return await _officeCollection.Find(_ => true).ToListAsync();
        }
    }
}
