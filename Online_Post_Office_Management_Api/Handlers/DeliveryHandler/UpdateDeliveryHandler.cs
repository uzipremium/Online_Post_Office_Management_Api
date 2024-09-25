using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Commands.DeliveryCommand;
using Online_Post_Office_Management_Api.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.DeliveryHandler
{
    public class UpdateDeliveryHandler : IRequestHandler<UpdateDelivery, bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public UpdateDeliveryHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<bool> Handle(UpdateDelivery request, CancellationToken cancellationToken)
        {
            // Validate input
            if (string.IsNullOrEmpty(request.Id))
            {
                throw new ArgumentException("Delivery ID must be provided.");
            }

            // Retrieve the existing delivery
            var delivery = await _deliveryRepository.GetById(request.Id);
            if (delivery == null)
            {
                return false;
            }

            // Update delivery properties
            delivery.SendDate = request.SendDate;
            delivery.DeliveryStatus = request.DeliveryStatus;
            delivery.StartOfficeId = request.StartOfficeId;
            delivery.CurrentLocation = request.CurrentLocation;
            delivery.EndOfficeId = request.EndOfficeId;

            // Attempt to update the delivery in the repository
            return await _deliveryRepository.Update(request.Id, delivery);
        }
    }
}
