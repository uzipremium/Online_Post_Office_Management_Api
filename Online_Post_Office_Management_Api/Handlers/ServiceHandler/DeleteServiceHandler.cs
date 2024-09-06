using MediatR;
using Online_Post_Office_Management_Api.Commands.ServiceCommand;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.ServiceHandler
{
    public class DeleteServiceHandler : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly IServiceRepository _serviceRepository;

        public DeleteServiceHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            return await _serviceRepository.Delete(request.Id);
        }
    }
}
