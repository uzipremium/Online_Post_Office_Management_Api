using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly IMongoCollection<Office> _offices;

        public OfficeRepository(IMongoDatabase database)
        {
            _offices = database.GetCollection<Office>("Offices");
        }

        public async Task<Office> GetById(string id)
        {
            return await _offices.Find(office => office.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Office>> GetAll()
        {
            return await _offices.Find(_ => true).ToListAsync();
        }
    }
}
