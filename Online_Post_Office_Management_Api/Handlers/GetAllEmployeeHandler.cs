using MediatR;
using Online_Post_Office_Management_Api.Queries;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Models;


namespace Online_Post_Office_Management_Api.Handlers
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
            var employees = await _employeeRepository.Get();
            return employees.ToList()   ;
        }
    }
}
