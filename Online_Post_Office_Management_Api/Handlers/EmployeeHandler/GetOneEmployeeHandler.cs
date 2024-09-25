using MediatR;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;
using System.Threading;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;

namespace Online_Post_Office_Management_Api.Handlers.EmployeeHandler
{
    public class GetOneEmployeeHandler : IRequestHandler<EmployeeGetOne, EmployeeWithOfficeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetOneEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeWithOfficeDto> Handle(EmployeeGetOne request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetById(request.Id);

       
            if (employee == null)
            {
              
                throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
            }

      
            var employeeDto = new EmployeeWithOfficeDto
            {
                Id = employee.Id,
                Email = employee.Email,
                Phone = employee.Phone,
                Gender = employee.Gender,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                CreatedDate = employee.CreatedDate,
                OfficeId = employee.OfficeId,
                AccountId = employee.AccountId,
    
            };

            return employeeDto;
        }
    }
}
