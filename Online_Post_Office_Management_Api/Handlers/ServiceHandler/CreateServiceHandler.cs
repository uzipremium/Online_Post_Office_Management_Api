using MediatR;
using Online_Post_Office_Management_Api.Commands.ServiceCommand;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.ServiceHandler
{
    public class CreateServiceHandler : IRequestHandler<CreateServiceCommand, bool>
    {
        private readonly IServiceRepository _serviceRepository;

        public CreateServiceHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<bool> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            await _serviceRepository.Create(request.Service);
            return true;
        }
    }
}
