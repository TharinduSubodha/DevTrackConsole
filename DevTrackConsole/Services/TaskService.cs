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

            //return _tasks.FirstOrDefault(task => task.Id == id);
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
        public List<WorkTask> GetTasksByPriority(TaskPriority priority)
        {
            return _tasks
                .Where(task => task.Priority == priority)
                .ToList();
        }

        public List<WorkTask> GetTasksByStatus(WorkTaskStatus status)
        {
            return _tasks
                .Where(task => task.Status == status)
                .ToList();
        }

        public List<WorkTask> GetTasksSortedByPriority()
        {
            return _tasks
                .OrderByDescending(task => task.Priority)
                .ToList();
        }

        public int GetCompletedTaskCount()
        {
            return _tasks
                .Count(task => task.Status == WorkTaskStatus.Done);
        }

        public List<WorkTask> GetPendingTasksSortedByPriority()
        {
            return _tasks
                .Where(task => task.Status != WorkTaskStatus.Done)
                .OrderByDescending(task => task.Priority)
                .ToList();
        }

        public List<string> GetHighPriorityTaskTitles()
        {
            return _tasks
                .Where(task => task.Priority == TaskPriority.High)
                .Select(task => task.Title)
                .ToList();
        }

        public Dictionary<WorkTaskStatus, int> GetTaskCountsByStatus()
        {
            return _tasks
                .GroupBy(task => task.Status)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        public bool HasUrgentPendingTasks()
        {
            return _tasks
                .Any(task => task.Priority == TaskPriority.High && task.Status != WorkTaskStatus.Done);
        }

        public List<WorkTask> GetTopFivePendingTasks()
        {
            return _tasks
                .Where(task => task.Status != WorkTaskStatus.Done)
                .OrderByDescending(task => task.Priority)
                .Take(5)
                .ToList();
        }

        public Dictionary<TaskPriority, int> GetTaskCountsByPriority()
        {
            return _tasks
                .GroupBy(task => task.Priority)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}
