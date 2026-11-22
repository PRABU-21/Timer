
using System;
using System.Collections.Generic;
using TaskTimerApp.Models;

namespace TaskTimerApp.Interfaces
{
    public interface ITaskRepository
    {
        void Add(TaskItem task);

        List<TaskItem> GetByUserId(Guid userId);
    }
}
