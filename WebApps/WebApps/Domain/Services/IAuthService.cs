using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Enum;
using WebApps.Domain.Models;
using WebApps.Domain.Services.Communication;

namespace WebApps.Domain.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> CreateUserAsync(User user, ERole[] userRoles, EType[] userTypes);
        Task<User> FindByEmailAsync(string email);
        Task<User> Authenticate(string email, string password);
    }
}
