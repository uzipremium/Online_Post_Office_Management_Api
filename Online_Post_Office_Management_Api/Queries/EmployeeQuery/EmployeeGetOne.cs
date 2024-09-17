using MediatR;
using Online_Post_Office_Management_Api.DTO;

namespace Online_Post_Office_Management_Api.Queries.EmployeeQuery
{
    public class EmployeeGetOne : IRequest<EmployeeWithOfficeDto>
    {
        public string Id { get; set; }

        public EmployeeGetOne(string id)
        {
            Id = id;
        }
    }
}
