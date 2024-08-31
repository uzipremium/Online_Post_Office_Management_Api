using MediatR;
using Online_Post_Office_Management_Api.Commands;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(DeleteEmployee request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.Delete(request.Id);
        }
    }
}
