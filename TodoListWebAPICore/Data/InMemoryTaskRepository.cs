using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListWebAPICore.Data.Entities;

namespace TodoListWebAPICore.Data
{
    public class InMemoryTaskRepository:ITaskRepository
    {
        readonly List<TodoTask> _tasks;

        public InMemoryTaskRepository()
        {
            if (_tasks == null)
            {
                _tasks = new List<TodoTask>()
                    {
                        new TodoTask { Id= 1, Description="Call Mum",Status=false},
                        new TodoTask { Id= 2, Description="Book holidays",Status=true},
                    };
            }
        }


        public TodoTask Add(TodoTask newTask)
        {
            if (newTask != null)
            {
                _tasks.Add(newTask);
                newTask.Id = _tasks.Max(r => r.Id) + 1;
                newTask.Status = false;
            }
            return newTask;
        }

        public IEnumerable<TodoTask> GetAll()
        {
            return _tasks.OrderByDescending(r => r.Description); 
        }

        public  Task<IEnumerable<TodoTask>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public TodoTask GetTaskById(int taskId)
        {
            return _tasks.SingleOrDefault(r => r.Id == taskId);
        }

        public Task<TodoTask> GetTaskByIdAsync(int taskId)
        {
            return Task.Run(() => GetTaskById(taskId));
        }

        public TodoTask Remove(int removeTaskId)
        {
            TodoTask _task = _tasks.SingleOrDefault(r => r.Id == removeTaskId);
            if (_task != null)
            {
                _tasks.Remove(_task);
            }
            return _task;
        }

        public TodoTask UpdateStatus(int updateTaskId)
        {
            TodoTask _task = _tasks.SingleOrDefault(r => r.Id == updateTaskId);
            if (_task != null)
            {
                _task.Status = !_task.Status;
            }
            return _task;

        }

    }
}
