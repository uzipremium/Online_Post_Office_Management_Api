using MediatR;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.EmployeeHandler
{
    public class SearchEmployeeQuery : IRequest<IEnumerable<EmployeeWithOfficeDto>>
    {
        public EmployeeSearch SearchCriteria { get; }

        public SearchEmployeeQuery(EmployeeSearch searchCriteria)
        {
            SearchCriteria = searchCriteria;
        }
    }

    public class SearchEmployeeHandler : IRequestHandler<SearchEmployeeQuery, IEnumerable<EmployeeWithOfficeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public SearchEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeWithOfficeDto>> Handle(SearchEmployeeQuery request, CancellationToken cancellationToken)
        {
            var searchCriteria = request.SearchCriteria;

            // Nếu tất cả các trường đều null hoặc rỗng, trả về tất cả nhân viên
            if (string.IsNullOrEmpty(searchCriteria.Name) &&
                string.IsNullOrEmpty(searchCriteria.OfficeId) &&
                string.IsNullOrEmpty(searchCriteria.Phone) &&
                string.IsNullOrEmpty(searchCriteria.OfficeName)) // Thêm kiểm tra OfficeName
            {
                return await _employeeRepository.GetAll();
            }

            // Gọi repository với các tham số
            var employees = await _employeeRepository.Search(
                searchCriteria.Name,
                searchCriteria.OfficeId,
                searchCriteria.Phone,
                searchCriteria.OfficeName // Thêm tìm kiếm theo OfficeName
            );

            return employees;
        }
    }
}
