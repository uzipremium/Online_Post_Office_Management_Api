using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands.ServiceCommand
{
    public class UpdateServiceCommand : IRequest<bool>
    {
        public Service Service { get; set; }

        public UpdateServiceCommand(Service service)
        {
            Service = service;
        }
    }
}
