using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Queries.CustomerQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class CheckPackageStatusQueryHandler : IRequestHandler<CheckPackageStatusQuery, PackageResponse>
    {
        private readonly IPackageRepository _packageRepository;

        public CheckPackageStatusQueryHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<PackageResponse> Handle(CheckPackageStatusQuery request, CancellationToken cancellationToken)
        {
            var packageResponse = await _packageRepository.GetByPackageIdAndPhone(request.PackageId, request.Phone);

            if (packageResponse == null)
            {
                return null;
            }

            return packageResponse;
        }
    }
}
