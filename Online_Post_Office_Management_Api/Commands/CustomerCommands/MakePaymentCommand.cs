using MediatR;

namespace Online_Post_Office_Management_Api.Commands.CustomerCommands
{
    public class MakePaymentCommand : IRequest<bool>
    {
        public string PackageId { get; set; }

        public MakePaymentCommand(string packageId)
        {
            PackageId = packageId;
        }
    }
}
