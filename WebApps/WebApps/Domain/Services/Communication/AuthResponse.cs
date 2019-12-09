using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Models;

namespace WebApps.Domain.Services.Communication
{
    public class AuthResponse
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public User User { get; private set; }

        private AuthResponse(bool success, string message, User user)
        {
            Success = success;
            Message = message;
            User = user;
        }

        public AuthResponse(User user) : this(true, string.Empty, user)
        { }

        public AuthResponse(string message) : this(false, message, null)
        { }
    }
}
