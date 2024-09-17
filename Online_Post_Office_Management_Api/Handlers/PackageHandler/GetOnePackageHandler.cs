using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Queries.PackageQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class GetOnePackageHandler : IRequestHandler<GetPackageByIdQuery, PackageResponse>
    {
        private readonly IPackageRepository _packageRepository;

        public GetOnePackageHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<PackageResponse> Handle(GetPackageByIdQuery request, CancellationToken cancellationToken)
        {
            var packageResponse = await _packageRepository.GetById(request.Id);
            return packageResponse;
        }
    }
}
