using MediatR;
using System;

namespace Online_Post_Office_Management_Api.Commands.DeliveryCommand
{
    public class UpdateDelivery : IRequest<bool>
    {
        public string Id { get; set; }
        public DateTime SendDate { get; set; }
        public string DeliveryStatus { get; set; }
        public string StartOfficeId { get; set; }
        public string CurrentLocation { get; set; }
        public string EndOfficeId { get; set; }
        public DateTime? DeliveryDate { get; set; }

        // Constructor
        public UpdateDelivery(string id, DateTime sendDate, string deliveryStatus, string startOfficeId, string currentLocation, string endOfficeId, DateTime? deliveryDate)
        {
            Id = id;
            SendDate = sendDate;
            DeliveryStatus = deliveryStatus;
            StartOfficeId = startOfficeId;
            CurrentLocation = currentLocation;
            EndOfficeId = endOfficeId;
            DeliveryDate = deliveryDate;
        }
    }
}
