
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TaskTimerApp.Interfaces;
using TaskTimerApp.Models;

namespace TaskTimerApp.Repositories
{

    public class JsonUserRepository : IUserRepository
    {
        private readonly string _filePath;
        private List<User> _users;

        public JsonUserRepository()
        {
            string dataFolder = Path.Combine(
                Directory.GetCurrentDirectory(), "Data");

            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "users.json");

            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                _users = JsonSerializer.Deserialize<List<User>>(json)
                         ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
                Save();
            }
        }

        //public User? GetByUsername(string username)
        public User GetByUsername(string username)
        {
            return _users.FirstOrDefault(
                u => u.Username.Equals(
                    username,
                    System.StringComparison.OrdinalIgnoreCase));
        }

        public void Add(User user)
        {
            _users.Add(user);
            Save();
        }

        private void Save()
        {
            string json = JsonSerializer.Serialize(
                _users,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_filePath, json);
        }
    }
}   