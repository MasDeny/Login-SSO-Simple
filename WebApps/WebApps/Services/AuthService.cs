using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Enum;
using WebApps.Domain.Models;
using WebApps.Domain.Repositories;
using WebApps.Domain.Security.Hashing;
using WebApps.Domain.Services;
using WebApps.Domain.Services.Communication;

namespace WebApps.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse> CreateUserAsync(User user, ERole[] userRoles, EType[] userTypes)
        {
            var existingUser = await _authRepository.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return new AuthResponse("Email already in use.");
            }

            user.Password = _passwordHasher.HashPassword(user.Password);

            await _authRepository.AddAsync(user, userRoles, userTypes);

            return new AuthResponse(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _authRepository.FindByEmailAsync(email);
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _authRepository.FindByEmailAsync(email);

            if (user == null || !_passwordHasher.PasswordMatches(password, user.Password))
            {
                return null;
            }

            return user;
        }
    }
}
