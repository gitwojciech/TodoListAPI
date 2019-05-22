using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListWebAPICore.Data.Entities;

namespace TodoListWebAPICore.Data
{
    public interface ITaskRepository
    {
        IEnumerable<TodoTask> GetAll();
        Task<IEnumerable<TodoTask>> GetAllAsync();
        TodoTask GetTaskById(int taskId);
        Task<TodoTask> GetTaskByIdAsync(int taskId);
        TodoTask Add(TodoTask newTask);
        TodoTask Remove(int removeTaskId);
        TodoTask UpdateStatus(int updateTaskId);
    }
}
