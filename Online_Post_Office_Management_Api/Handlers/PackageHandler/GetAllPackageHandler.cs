using MediatR;
using MongoDB.Bson;
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

            // Apply filters if provided
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

            // Apply pagination with fixed pageSize of 10
            var pagedPackages = allPackages
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            return pagedPackages;
        }
    }
}
