
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TaskTimerApp.Interfaces;
using TaskTimerApp.Models;

namespace TaskTimerApp.Repositories
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;
        private List<TaskItem> _tasks;

        public JsonTaskRepository()
        {
            string dataFolder = Path.Combine(
                Directory.GetCurrentDirectory(), "Data");

            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "tasks.json");

            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                _tasks = JsonSerializer.Deserialize<List<TaskItem>>(json)
                         ?? new List<TaskItem>();
            }
            else
            {
                _tasks = new List<TaskItem>();
                Save();
            }
        }

        public void Add(TaskItem task)
        {
            _tasks.Add(task);
            Save();
        }

        public List<TaskItem> GetByUserId(Guid userId)
        {
            return _tasks
                .Where(t => t.UserId == userId)
                .ToList();
        }

        private void Save()
        {
            string json = JsonSerializer.Serialize(
                _tasks,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_filePath, json);
        }
    }
}
