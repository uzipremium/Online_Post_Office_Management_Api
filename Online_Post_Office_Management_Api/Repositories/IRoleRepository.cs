﻿using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetById(string id);
        Task<IEnumerable<Role>> GetAll();
    }
}
