using MediatR;

namespace Online_Post_Office_Management_Api.Commands.EmployeeCommand
{
    public class DeleteEmployee_Account : IRequest<bool>
    {
        public string Id { get; }

        public DeleteEmployee_Account(string employeeId)
        {
            Id = employeeId;
        }
    }
}
