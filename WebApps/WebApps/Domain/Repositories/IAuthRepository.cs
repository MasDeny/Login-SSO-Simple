using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Enum;
using WebApps.Domain.Models;

namespace WebApps.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<IEnumerable<User>> ListAsync();  // GetAll
        Task<User> FindByEmailAsync(string email);
        Task AddAsync(User user, ERole[] userRoles, EType[] userTypes);
    }
}
