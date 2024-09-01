using MediatR;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Queries.EmployeeQuery
{
    public class EmployeeGetOne : IRequest<Employee>
    {
        public string Id { get; set; }

        public EmployeeGetOne(string id)
        {
            Id = id;
        }
    }
}
