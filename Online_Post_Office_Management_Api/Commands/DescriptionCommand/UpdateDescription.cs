using MediatR;

namespace Online_Post_Office_Management_Api.Commands.DeliveryCommand
{
    public class Update_Description : IRequest<bool>
    {
        public string Id { get; set; }
        public string DescriptionText { get; set; }
        public string ReceiverAddress { get; set; }

        public Update_Description(string id, string descriptionText, string receiverAddress)
        {
            Id = id;
            DescriptionText = descriptionText;
            ReceiverAddress = receiverAddress;
        }
    }
}
