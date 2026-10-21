using DevTrackConsole.Enums;
using DevTrackConsole.Models;
using System.Collections.Generic;

namespace DevTrackConsole.Services
{
    public class TaskService
    {
        private readonly List<WorkTask> _tasks = new List<WorkTask>();
        private int _nextId = 1;
        public void AddTask(WorkTask task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
        }
         

        public List<WorkTask> GetAllTasks()
        {
            return new List<WorkTask>(_tasks);
        }


        public WorkTask? GetTaskById(int id)
        {
            foreach (WorkTask task in _tasks) {
            if (task.Id == id){
                    return task;
                }}
            return null;

        }

        public bool UpdateTaskStatus(int id, WorkTaskStatus newStatus)
        {
            var task = GetTaskById(id);
            if (task != null)
            {
                task.Status = newStatus;
                return true;
            }
            return false;
        }

        public bool DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task != null)
            {
                _tasks.Remove(task);
                return true;
            }
            return false;
        }
    }
}
