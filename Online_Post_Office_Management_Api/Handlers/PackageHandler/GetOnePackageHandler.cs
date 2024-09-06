using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.PackageQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class GetOnePackageHandler : IRequestHandler<PackageGetOne, Package>
    {
        private readonly IPackageRepository _packageRepository;

        public GetOnePackageHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<Package> Handle(PackageGetOne request, CancellationToken cancellationToken)
        {
            var package = await _packageRepository.GetById(request.Id);
            return package;
        }
    }
}
