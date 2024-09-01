using MediatR;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Queries.DescriptionQuery
{
    public class DescriptionGetAll : IRequest<List<Description>>
    {
    }
}
