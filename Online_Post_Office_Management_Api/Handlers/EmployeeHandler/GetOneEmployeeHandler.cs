using MediatR;
using Online_Post_Office_Management_Api.Models;
using System.Threading;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;


namespace Online_Post_Office_Management_Api.Handlers.EmployeeHandler
{
    public class GetOneEmployeeHandler : IRequestHandler<EmployeeGetOne, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetOneEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> Handle(EmployeeGetOne request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetById(request.Id);
        }
    }
}
