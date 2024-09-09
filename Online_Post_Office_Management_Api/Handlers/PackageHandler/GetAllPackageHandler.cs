using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.PackageQuery;
using Online_Post_Office_Management_Api.Repositories;


namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class GetAllPackageHandler : IRequestHandler<PackageGetAll, List<Package>>
    {
        private readonly IPackageRepository _packageRepository;

        public GetAllPackageHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<List<Package>> Handle(PackageGetAll request, CancellationToken cancellationToken)
        {
            var packages = await _packageRepository.GetAll();
            return packages.ToList();
        }
    }
}
