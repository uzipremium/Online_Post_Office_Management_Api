using MediatR;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Queries.PackageQuery
{
    public class PackageGetOne : IRequest<Package>
    {
        public string Id { get; set; }

        public PackageGetOne(string id)
        {
            Id = id;
        }
    }
}
