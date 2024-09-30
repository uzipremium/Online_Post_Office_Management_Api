using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Exceptions;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class UpdatePackageHandler : IRequestHandler<UpdatePackage, bool>
    {
        private readonly IPackageRepository _packageRepository;

        public UpdatePackageHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<bool> Handle(UpdatePackage request, CancellationToken cancellationToken)
        {
            var package = await _packageRepository.GetPackageById(request.Id);
            if (package == null)
            {
                return false;
            }

            if (package.Weight == (decimal)request.Weight &&
                package.Distance == (decimal)request.Distance &&
                package.DeliveryNumber == request.DeliveryNumber &&
                package.Receiver == request.Receiver)
            {
                throw new NoChangeException();
            }

            package.Weight = (decimal)request.Weight;
            package.Distance = (decimal)request.Distance;
            package.DeliveryNumber = request.DeliveryNumber;
            package.Receiver = request.Receiver;

            var updateResult = await _packageRepository.Update(request.Id, package);

            return updateResult;
        }
    }
}
