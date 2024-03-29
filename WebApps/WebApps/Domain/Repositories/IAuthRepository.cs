﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Enum;
using WebApps.Domain.Models;

namespace WebApps.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<IEnumerable<Models.Type>> ListUrlAsync();  // GetAll
        Task<IEnumerable<Role>> ListRoleAsync();

        Task<User> FindByEmailAsync(string email);
        Task AddAsync(User user, ERole[] userRoles, EType[] userTypes);
        void ChangePasswordAsync(User user);
    }
}
