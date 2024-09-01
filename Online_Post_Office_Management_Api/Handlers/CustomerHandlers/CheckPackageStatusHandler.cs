using MediatR;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQueries;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class CheckPackageStatusHandler : IRequestHandler<CheckPackageStatusQuery, Package>
    {
        private readonly IMongoCollection<Package> _packages;
        private readonly IMongoCollection<Customer> _customer;

        public CheckPackageStatusHandler(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("qwer");
            _packages = database.GetCollection<Package>("Package");
            _customer = database.GetCollection<Customer>("Customer");
        }
        public async Task<Package> Handle(CheckPackageStatusQuery request, CancellationToken cancellationToken)
        {
            var aggregate = _packages.Aggregate()
                .Match(p => p.Id == request.PackageId)
                .Lookup<Package, Customer, Package>(
                    _customer,
                    p => p.SenderId,
                    c => c.Id,
                    result => result.Sender
                )
                .As<Package>();

            var package = await aggregate.FirstOrDefaultAsync(cancellationToken);

            if (package?.Sender == null || package.Sender.Phone != request.Phone)
            {
                return null;
            }

            return package;
        }
    }
}
