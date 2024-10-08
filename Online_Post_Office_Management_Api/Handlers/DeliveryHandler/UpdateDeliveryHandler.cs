﻿using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Commands.DeliveryCommand;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Exceptions;

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
            var delivery = await _deliveryRepository.GetById(request.Id);

            if (delivery == null)
            {
                return false; // Delivery does not exist
            }

            // Check if there are any changes, handle nullable DeliveryDate
            bool hasChanges = delivery.DeliveryStatus != request.DeliveryStatus ||
                              delivery.StartOfficeId != request.StartOfficeId ||
                              delivery.CurrentLocation != request.CurrentLocation ||
                              delivery.EndOfficeId != request.EndOfficeId;

            if (!hasChanges)
            {
                // If there are no changes, throw NoChangeException
                throw new NoChangeException();
            }

            // Update the delivery properties
            delivery.SendDate = request.SendDate;
            delivery.DeliveryStatus = request.DeliveryStatus;
            delivery.StartOfficeId = request.StartOfficeId;
            delivery.CurrentLocation = request.CurrentLocation;
            delivery.EndOfficeId = request.EndOfficeId;

            // Attempt to update the delivery
            return await _deliveryRepository.Update(request.Id, delivery);
        }
    }
}
