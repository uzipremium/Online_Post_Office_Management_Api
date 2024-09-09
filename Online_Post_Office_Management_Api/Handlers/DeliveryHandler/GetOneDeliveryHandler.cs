using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.Deliveries;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.DeliveryHandler
{
    public class GetOneDeliveryHandler : IRequestHandler<DeliveryGetOne, Delivery>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public GetOneDeliveryHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<Delivery> Handle(DeliveryGetOne request, CancellationToken cancellationToken)
        {
            return await _deliveryRepository.GetById(request.Id);
        }
    }
}
