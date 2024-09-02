using MediatR;

namespace Online_Post_Office_Management_Api.Commands.ServiceCommand
{
    public class DeleteServiceCommand : IRequest<bool>
    {
        public string Id { get; }

        public DeleteServiceCommand(string id)
        {
            Id = id;
        }
    }
}
