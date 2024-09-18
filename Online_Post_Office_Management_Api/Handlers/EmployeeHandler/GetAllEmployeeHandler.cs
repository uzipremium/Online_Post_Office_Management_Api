using MediatR;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.EmployeeHandler
{
    public class GetAllEmployeeHandler : IRequestHandler<EmployeeGetAll, List<EmployeeWithOfficeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeWithOfficeDto>> Handle(EmployeeGetAll request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAll();
            return employees.ToList();
        }
    }
}
