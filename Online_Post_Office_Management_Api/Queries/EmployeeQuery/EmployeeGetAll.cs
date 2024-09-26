using MediatR;
using Online_Post_Office_Management_Api.DTO;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Queries.EmployeeQuery
{
    public class EmployeeGetAll : IRequest<List<EmployeeWithOfficeDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public EmployeeGetAll(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
