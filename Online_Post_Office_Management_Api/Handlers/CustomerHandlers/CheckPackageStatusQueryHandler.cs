using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Queries.CustomerQuery;
using Online_Post_Office_Management_Api.Repositories;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class CheckPackageStatusQueryHandler : IRequestHandler<CheckPackageStatusQuery, PackageResponse>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ILogger<CheckPackageStatusQueryHandler> _logger;

        public CheckPackageStatusQueryHandler(IPackageRepository packageRepository, ILogger<CheckPackageStatusQueryHandler> logger)
        {
            _packageRepository = packageRepository;
            _logger = logger;
        }

        public async Task<PackageResponse> Handle(CheckPackageStatusQuery request, CancellationToken cancellationToken)
        {
  
            if (string.IsNullOrEmpty(request.PackageId))
            {
                throw new ArgumentException("Package ID must be provided.");
            }

       
            if (!Regex.IsMatch(request.Phone, @"^\d{10,15}$"))
            {
                throw new ArgumentException("Phone number must be between 10 and 15 digits and contain only numbers.");
            }

            // Fetch package status
            var packageResponse = await _packageRepository.GetByPackageIdAndPhone(request.PackageId, request.Phone);
            if (packageResponse == null)
            {
                _logger.LogWarning("Package not found: Package ID: {PackageId}, Phone: {Phone}", request.PackageId, request.Phone);
                throw new KeyNotFoundException("Package not found for the provided ID and phone number.");
            }

            return packageResponse;
        }
    }
}
