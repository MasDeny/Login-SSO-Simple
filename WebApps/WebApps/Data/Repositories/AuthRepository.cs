using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Data.Context;
using WebApps.Domain.Enum;
using WebApps.Domain.Models;
using WebApps.Domain.Repositories;

namespace WebApps.Data.Repositories
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public AuthRepository(WebAppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            var result = await _context.Users
                .Where(item => EF.Property<bool>(item, "Void") == false)
                .ToListAsync();
            return result;// await _context.Departments.ToListAsync();
        }

        public async Task AddAsync(User user, ERole[] userRoles, EType[] userTypes)
        {
            var roles = await _context.Roles.Where(r => userRoles.Any(ur => ur.ToString() == r.Name))
                .ToListAsync();

            var types = await _context.Types.Where(r => userTypes.Any(ur => ur.ToString() == r.Name))
                .ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole { RoleId = role.Id });
            }

            foreach (var type in types)
            {
                user.UserTypes.Add(new UserType { TypeId = type.Id });
            }

            var x = _context.Users.Add(user).Context.SaveChanges();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Include(u => u.UserTypes)
                .ThenInclude(ur => ur.Type)
                .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
