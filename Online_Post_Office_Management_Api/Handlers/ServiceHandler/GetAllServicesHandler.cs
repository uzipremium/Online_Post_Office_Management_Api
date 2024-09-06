using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.ServiceQuery;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.ServiceHandler
{
    public class GetAllServicesHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<Service>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServicesHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<Service>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            return await _serviceRepository.GetAll();
        }
    }
}
