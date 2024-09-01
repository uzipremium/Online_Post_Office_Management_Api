using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.EmployeeHandler
{
    public class GetAllEmployeeHandler : IRequestHandler<EmployeeGetAll, List<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> Handle(EmployeeGetAll request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAll();
            return employees.ToList();
        }
    }
}
