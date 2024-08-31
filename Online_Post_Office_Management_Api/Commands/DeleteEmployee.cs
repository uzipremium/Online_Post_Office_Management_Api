using MediatR;

namespace Online_Post_Office_Management_Api.Commands
{
    public class DeleteEmployee : IRequest<bool>
    {
        public string Id { get; }

        public DeleteEmployee(string id)
        {
            Id = id;
        }
    }
}
