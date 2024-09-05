using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.ServiceQuery;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.ServiceHandler
{
    public class GetOneServiceHandler : IRequestHandler<GetServiceQuery, Service>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetOneServiceHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Service> Handle(GetServiceQuery request, CancellationToken cancellationToken)
        {
            return await _serviceRepository.GetById(request.Id);
        }
    }
}
