
using System;

namespace TaskTimerApp.Models
{

    public class User
    {
        public Guid UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
