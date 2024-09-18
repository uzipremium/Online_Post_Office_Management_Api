using MediatR;
using Online_Post_Office_Management_Api.DTO;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Queries.EmployeeQuery
{
    public class EmployeeGetAll : IRequest<List<EmployeeWithOfficeDto>>
    {
    }
}
