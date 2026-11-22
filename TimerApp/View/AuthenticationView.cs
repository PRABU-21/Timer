
using System;
using TaskTimerApp.Models;
using TaskTimerApp.Services;

namespace TaskTimerApp.Views
{

    public class AuthenticationView
    {
        private readonly AuthenticationService _authService;

        public AuthenticationView(AuthenticationService authService)
        {
            _authService = authService;
        }

        public User ShowLoginPage()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("========== TASK TIMER ==========");
                Console.WriteLine("           LOGIN PAGE");
                Console.WriteLine();
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("3. Exit");
                Console.WriteLine();

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        User user = Login();
                        if (user != null)
                        {
                            return user;
                        }
                        break;

                    case "2":
                        SignUp();
                        break;

                    case "3":
                        return null;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private User Login()
        {
            Console.Clear();

            Console.WriteLine("========== LOGIN ==========");

            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            User user = _authService.Login(username, password);

            if (user == null)
            {
                Console.WriteLine("\nInvalid username or password.");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine("\nLogin successful!");
            Console.ReadKey();

            return user;
        }

        private void SignUp()
        {
            Console.Clear();

            Console.WriteLine("========== SIGN UP ==========");

            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            bool success = _authService.SignUp(
                username,
                password,
                out string message
            );

            Console.WriteLine("\n" + message);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}