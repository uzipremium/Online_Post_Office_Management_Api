﻿using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.CustomerQuery
{
    public class CheckPackageStatusQuery : IRequest<PackageResponse>
    {
        public string Phone { get; set; }
        public string PackageId { get; set; }

        public CheckPackageStatusQuery(string phone, string packageId)
        {
            Phone = phone;
            PackageId = packageId;
        }
    }
}
