using MediatR;
using Online_Post_Office_Management_Api.Commands.ServiceCommand;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.ServiceHandler
{
    public class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand, bool>
    {
        private readonly IServiceRepository _serviceRepository;

        public UpdateServiceHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<bool> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            // Get current service information from database based on ID
            var service = await _serviceRepository.GetById(request.Service.Id);

            if (service == null)
            {
                // If service not found, return false
                return false;
            }

            // Cập nhật thông tin của service
            service.Name = request.Service.Name;
            service.BaseRate = request.Service.BaseRate;
            service.RatePerKg = request.Service.RatePerKg;
            service.RatePerKm = request.Service.RatePerKm;

            // Perform service updates in the database
            return await _serviceRepository.Update(request.Service.Id, service);
        }
    }
}
