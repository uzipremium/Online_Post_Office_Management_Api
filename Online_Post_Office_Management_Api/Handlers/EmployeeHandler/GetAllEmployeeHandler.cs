using MediatR;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Collections.Generic;
using System.Linq;
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
            // Gọi phương thức GetAll từ repository với tham số phân trang
            var employees = await _employeeRepository.GetAll(request.PageNumber, request.PageSize);

            // Chuyển đổi sang DTO
            var employeeDtos = employees.Select(employee => new EmployeeWithOfficeDto
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
            }).ToList();

            return employeeDtos;
        }
    }
}
