using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class CheckPackageStatusQueryHandler : IRequestHandler<CheckPackageStatusQuery, Package>
    {
        private readonly IPackageRepository _packageRepository;

        public CheckPackageStatusQueryHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<Package> Handle(CheckPackageStatusQuery request, CancellationToken cancellationToken)
        {
            var package = await _packageRepository.GetById(request.PackageId);

            if (package == null || package.SenderId != request.Phone)
            {
                return null;
            }

            return package;
        }
    }
}
