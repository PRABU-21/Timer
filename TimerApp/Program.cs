
using System;
using TaskTimerApp.Interfaces;
using TaskTimerApp.Repositories;
using TaskTimerApp.Services;
using TaskTimerApp.Views;

namespace TaskTimerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Authentication setup
            IUserRepository userRepository =
                new JsonUserRepository();

            AuthenticationService authService =
                new AuthenticationService(userRepository);

            AuthenticationView authView =
                new AuthenticationView(authService);

            // Task management setup
            ITaskRepository taskRepository =
                new JsonTaskRepository();

            TimerService timerService = new TimerService();

            MainView mainView =
                new MainView(taskRepository, timerService);

            // Application workflow
            while (true)
            {
                var loggedInUser = authView.ShowLoginPage();

                if (loggedInUser == null)
                {
                    break;
                }

                mainView.ShowMainPage(loggedInUser);
            }

            Console.WriteLine("\nThank you for using Task Timer!");
        }
    }
}
