
using System;
using System.Threading;
using TaskTimerApp.Interfaces;
using TaskTimerApp.Models;
using TaskTimerApp.Services;

namespace TaskTimerApp.Views
{
    public class MainView
    {
        private readonly ITaskRepository _taskRepository;
        private readonly TimerService _timerService;

        public MainView(
            ITaskRepository taskRepository,
            TimerService timerService)
        {
            _taskRepository = taskRepository;
            _timerService = timerService;
        }

        public void ShowMainPage(User user)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("========== TASK TIMER ==========");
                Console.WriteLine($"Welcome, {user.Username}!");
                Console.WriteLine();
                Console.WriteLine("1. New Task");
                Console.WriteLine("2. Time Logs (Coming Soon)");
                Console.WriteLine("3. Logout");
                Console.WriteLine();

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        CreateTask(user);
                        break;

                    case "2":
                        Console.WriteLine("Time Logs will be implemented later.");
                        Console.ReadKey();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateTask(User user)
        {
            Console.Clear();

            Console.WriteLine("========== NEW TASK ==========");
            Console.Write("Enter task name: ");

            string taskName = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(taskName))
            {
                Console.WriteLine("Task name cannot be empty.");
                Console.ReadKey();
                return;
            }

            TaskItem task = new TaskItem
            {
                TaskId = Guid.NewGuid(),
                UserId = user.UserId,
                TaskName = taskName.Trim(),
                TotalDuration = TimeSpan.Zero
            };

            Console.WriteLine();
            Console.WriteLine("Task created successfully!");
            Console.WriteLine();
            Console.WriteLine("Press ENTER to start the timer.");
            Console.WriteLine("Press any other key to cancel.");

            ConsoleKey key = Console.ReadKey(true).Key;

            if (key != ConsoleKey.Enter)
            {
                Console.WriteLine("Task cancelled.");
                Console.ReadKey();
                return;
            }

            StartTaskTimer(task);
        }

        private void StartTaskTimer(TaskItem task)
        {
            Console.Clear();

            Console.WriteLine("========== TASK TIMER ==========");
            Console.WriteLine($"Task: {task.TaskName}");
            Console.WriteLine();

            _timerService.Start();

            Console.WriteLine("Timer started!");
            Console.WriteLine("Press any key to stop the timer.");
            Console.WriteLine();

            while (!Console.KeyAvailable)
            {
                TimeSpan elapsed = _timerService.GetElapsedTime();

                string formattedTime =
                    $"{(int)elapsed.TotalHours:D2}:" +
                    $"{elapsed.Minutes:D2}:" +
                    $"{elapsed.Seconds:D2}";

                Console.Write($"\rElapsed Time: {formattedTime}");

                Thread.Sleep(200);
            }

            Console.ReadKey(true);

            TimeSpan duration = _timerService.Stop();

            task.TotalDuration += duration;

            _taskRepository.Add(task);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Timer stopped!");
            Console.WriteLine($"Task: {task.TaskName}");
            Console.WriteLine($"Recorded Time: {duration}");
            Console.WriteLine("Time saved successfully!");

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
    