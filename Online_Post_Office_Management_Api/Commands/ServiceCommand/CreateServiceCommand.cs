using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands.ServiceCommand
{
    public class CreateServiceCommand : IRequest<bool>
    {
        public Service Service { get; }

        public CreateServiceCommand(Service service)
        {
            Service = service;
        }
    }
}
