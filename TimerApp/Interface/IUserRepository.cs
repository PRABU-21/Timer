
using TaskTimerApp.Models;


namespace TaskTimerApp.Interfaces
{

    public interface IUserRepository
    {
        User GetByUsername(string username);

        void Add(User user);
    }

}