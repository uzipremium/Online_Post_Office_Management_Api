using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetOnePackageHandler> _logger;

        public GetOnePackageHandler(IPackageRepository packageRepository, ILogger<GetOnePackageHandler> logger)
        {
            _packageRepository = packageRepository;
            _logger = logger;
        }

        public async Task<PackageResponse> Handle(GetPackageByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching package with ID: {request.Id}");

            var packageResponse = await _packageRepository.GetById(request.Id);

            if (packageResponse == null)
            {
                _logger.LogWarning($"Package with ID {request.Id} not found.");
          
                return null; 
            }

            _logger.LogInformation($"Package with ID {request.Id} retrieved successfully.");
            return packageResponse;
        }
    }
}
