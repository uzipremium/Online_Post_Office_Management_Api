using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.Deliveries;
using Online_Post_Office_Management_Api.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

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
       
            if (string.IsNullOrEmpty(request.Id))
            {
                throw new ArgumentException("Delivery ID must be provided.");
            }

            // Attempt to retrieve the delivery
            var delivery = await _deliveryRepository.GetById(request.Id);
            if (delivery == null)
            {
                throw new KeyNotFoundException($"No delivery found with ID: {request.Id}");
            }

            return delivery;
        }
    }
}
