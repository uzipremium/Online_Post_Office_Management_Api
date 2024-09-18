using MediatR;
using System;

namespace Online_Post_Office_Management_Api.Commands.PackageCommand
{
    public class UpdatePackage : IRequest<bool>
    {
        public string Id { get; set; }
        public double Weight { get; set; }
        public decimal Distance { get; set; }
        public string DeliveryNumber { get; set; }
        public string Receiver { get; set; }
        public DateTime CreatedAt { get; set; }

        public UpdatePackage(string id, double weight, decimal distance, string deliveryNumber, string receiver, DateTime createdAt)
        {
            Id = id;
            Weight = weight;
            Distance = distance;
            DeliveryNumber = deliveryNumber;
            Receiver = receiver;
            CreatedAt = createdAt;
        }
    }
}
