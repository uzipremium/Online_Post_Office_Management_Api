using MongoDB.Driver;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class PackageRepository : IPackageRepository
    {
        private readonly IMongoCollection<Package> _packages;
        private readonly IMongoCollection<Customer> _customers;
        private readonly IMongoCollection<Office> _offices;
        private readonly IMongoCollection<Service> _services;
        private readonly IMongoCollection<Description> _descriptions;
        private readonly IMongoCollection<Payment> _payments;
        private readonly IMongoCollection<Delivery> _deliveries;
        
        public PackageRepository(IMongoDatabase database)
        {
            _packages = database.GetCollection<Package>("Packages");
            _customers = database.GetCollection<Customer>("Customers");
            _offices = database.GetCollection<Office>("Offices");
            _services = database.GetCollection<Service>("Services");
            _descriptions = database.GetCollection<Description>("Descriptions");
            _payments = database.GetCollection<Payment>("Payments");
            _deliveries = database.GetCollection<Delivery>("Deliveries");
        }

        public async Task<PackageResponse> GetById(string id)
        {
            var package = await _packages.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (package == null)
                return null;

             var delivery = await _deliveries.Find(d => d.Id == package.DeliveryId).FirstOrDefaultAsync();

            // Fetch End Office Name using EndOfficeId from delivery
            var endOffice = await _offices.Find(o => o.Id == delivery.EndOfficeId).FirstOrDefaultAsync();

            // Convert Package to PackageResponse
            var packageResponse = await FetchRelatedData(package);
            return packageResponse;
        }

        public async Task<IEnumerable<PackageResponse>> GetAll()
        {
            var packages = await _packages.Find(_ => true)
                                  .SortByDescending(p => p.CreatedAt)
                                  .ToListAsync();

            var packageResponses = new List<PackageResponse>();

            foreach (var package in packages)
            {
                var packageResponse = await FetchRelatedData(package);
                packageResponses.Add(packageResponse);
            }

            return packageResponses;
        }


        public async Task Create(Package package)
        {
            if (string.IsNullOrEmpty(package.Id))
            {
                package.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            }

            await _packages.InsertOneAsync(package);
        }

        public async Task<bool> Update(string id, Package package)
        {
            var result = await _packages.ReplaceOneAsync(p => p.Id == id, package);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _packages.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<Package> GetPackageById(string id)
        {
            return await _packages.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PackageResponse> GetByPackageIdAndPhone(string packageId, string phone)
        {
            if (string.IsNullOrEmpty(packageId))
            {
                throw new ArgumentException("Package ID cannot be empty.");
            }

            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentException("Phone cannot be empty.");
            }

            var package = await _packages.Find(p => p.Id == packageId)
                                             .SortByDescending(p => p.CreatedAt)
                                             .FirstOrDefaultAsync();
            if (package == null)
            {
                throw new KeyNotFoundException("Not Found Package");
            }

            var sender = await _customers.Find(s => s.Id == package.SenderId && s.Phone == phone).FirstOrDefaultAsync();

            if (sender == null)
            {
                throw new KeyNotFoundException("Phone number does not match the sender of the package");
            }

            var packageResponse = await FetchRelatedData(package);

            packageResponse.SenderName = sender.Name;
            packageResponse.CustomerPhone = sender.Phone;
            packageResponse.CustomerEmail = sender.Email;

            return packageResponse;
        }



        private async Task<PackageResponse> FetchRelatedData(Package package)
        {
            // Fetch related data
            var sender = await _customers.Find(s => s.Id == package.SenderId).FirstOrDefaultAsync();
            var office = await _offices.Find(o => o.Id == package.OfficeId).FirstOrDefaultAsync();
            var service = await _services.Find(s => s.Id == package.ServiceId).FirstOrDefaultAsync();
            var description = await _descriptions.Find(d => d.Id == package.DescriptionId).FirstOrDefaultAsync();
            var payment = await _payments.Find(p => p.Id == package.PaymentId).FirstOrDefaultAsync();
            var delivery = await _deliveries.Find(d => d.Id == package.DeliveryId).FirstOrDefaultAsync();

            // Convert each Package to PackageResponse
            return new PackageResponse
            {
                Id = package.Id,
                SenderName = sender?.Name,
                CustomerPhone = sender?.Phone,
                CustomerEmail = sender?.Email,
                OfficeId = office?.Id,
                OfficeName = office?.OfficeName,
                OfficeAddress = office?.Address,
                OfficeState = office?.State,
                OfficeCity = office?.City,
                OfficePinCode = office?.PinCode,
                OfficePhone = office?.Phone,
                ServiceName = service?.Name,
                Weight = package.Weight,
                Distance = package.Distance,
                DeliveryNumber = package.DeliveryNumber,
                DescriptionText = description.DescriptionText,
                ReceiverAddress = description.ReceiverAddress,
                PaymentStatus = payment.Status,
                TransactionTime = payment.TransactionTime,
                PaymentCost = payment.Cost,
                SendDate = delivery.SendDate,
                DeliveryStatus = delivery.DeliveryStatus,
                EndOfficeName = delivery?.EndOfficeId,
                DeliveryDate = delivery.DeliveryDate,
                Receiver = package.Receiver,
                CreatedAt = package.CreatedAt
            };
        }

    }
}
