using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class UpdatePackageHandler : IRequestHandler<UpdatePackage, bool>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ILogger<UpdatePackageHandler> _logger;

        public UpdatePackageHandler(IPackageRepository packageRepository, ILogger<UpdatePackageHandler> logger)
        {
            _packageRepository = packageRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdatePackage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to update package with ID: {request.Id}");

            var package = await _packageRepository.GetPackageById(request.Id);
            if (package == null)
            {
                _logger.LogWarning($"Package with ID {request.Id} not found for update.");
                return false;
            }

            package.Weight = (decimal)request.Weight;
            package.Distance = (decimal)request.Distance;
            package.DeliveryNumber = request.DeliveryNumber;
            package.Receiver = request.Receiver;

            bool updateResult = await _packageRepository.Update(request.Id, package);

            if (updateResult)
            {
                _logger.LogInformation($"Package with ID {request.Id} updated successfully.");
            }
            else
            {
                _logger.LogError($"Failed to update package with ID {request.Id}.");
            }

            return updateResult;
        }
    }
}
