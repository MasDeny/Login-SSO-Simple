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

        public async Task<AuthResponse> ChangePasswordAsync(string email, string password, string passwordReset)
        {
            var userExisting = await _authRepository.FindByEmailAsync(email);

            if (!_passwordHasher.PasswordMatches(password, userExisting.Password))
            {
                return null;
            }

            userExisting.Password = _passwordHasher.HashPassword(passwordReset);

            try
            {
                _authRepository.ChangePasswordAsync(userExisting);
                return new AuthResponse(userExisting);
            }

            catch (Exception ex)
            {
                // Do some logging stuff
                return new AuthResponse($"An error occurred when updating the schedule: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Domain.Models.Type>> ListUrlAsync()
        {
            return await _authRepository.ListUrlAsync();

        }

        public async Task<IEnumerable<Role>> ListRoleAsync()
        {
            return await _authRepository.ListRoleAsync();

        }
    }
}
