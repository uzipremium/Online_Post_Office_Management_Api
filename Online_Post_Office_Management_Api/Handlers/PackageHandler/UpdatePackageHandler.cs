using MediatR;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Repositories;

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

            package.Weight = (decimal)request.Weight;
            package.DeliveryNumber = request.DeliveryNumber;
            package.Receiver = request.Receiver;

            return await _packageRepository.Update(request.Id, package);
        }
    }
}
