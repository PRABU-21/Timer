
using System;
using System.Linq;
using TaskTimerApp.Interfaces;
using TaskTimerApp.Models;

namespace TaskTimerApp.Services
{
    public class AuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool SignUp(string username, string password, out string message)
        {
            username = username.Trim();

            if (username.Length < 3)
            {
                message = "Username must contain at least 3 characters.";
                return false;
            }

            if (username.Any(char.IsWhiteSpace))
            {
                message = "Username cannot contain spaces.";
                return false;
            }

            if (password.Length < 8)
            {
                message = "Password must contain at least 8 characters.";
                return false;
            }

            if (_userRepository.GetByUsername(username) != null)
            {
                message = "Username already exists.";
                return false;
            }

            User user = new User
            {
                UserId = Guid.NewGuid(),
                Username = username,
                Password = password
            };

            _userRepository.Add(user);

            message = "Registration successful!";
            return true;
        }

        public User Login(string username, string password)
        {
            username = username.Trim();

            User user = _userRepository.GetByUsername(username);

            if (user == null)
            {
                return null;
            }

            if (user.Password != password)
            {
                return null;
            }

            return user;
        }
    }
}
