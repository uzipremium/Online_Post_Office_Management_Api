using MediatR;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Queries
{
    public class EmployeeGetOne: IRequest<Employee>
    {
        public string Id { get; set; }

        public EmployeeGetOne(string id)
        {
            Id = id;
        }
    }
}
