using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Queries.PackageQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class GetAllPackagesHandler : IRequestHandler<GetAllPackagesQuery, IEnumerable<PackageResponse>>
    {
        private readonly IPackageRepository _packageRepository;

        public GetAllPackagesHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<IEnumerable<PackageResponse>> Handle(GetAllPackagesQuery request, CancellationToken cancellationToken)
        {
            var allPackages = await _packageRepository.GetAll();

            if (!string.IsNullOrEmpty(request.OfficeId))
            {
                allPackages = allPackages.Where(p => p.OfficeId == request.OfficeId);
            }

            if (request.StartDate.HasValue)
            {
                allPackages = allPackages.Where(p => p.CreatedAt >= request.StartDate.Value);
            }

            if (!string.IsNullOrEmpty(request.PaymentStatus))
            {
                allPackages = allPackages.Where(p => p.PaymentStatus == request.PaymentStatus);
            }

     
            int pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;
            int pageSize = request.PageSize > 0 ? request.PageSize : 10;

            var pagedPackages = allPackages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return pagedPackages;
        }
    }
}
