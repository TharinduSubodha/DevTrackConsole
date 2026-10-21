using DevTrackConsole.Enums;
using DevTrackConsole.Models;
using DevTrackConsole.Services;

// Create TaskService object
TaskService taskService = new TaskService();

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("=== DEVTRACK TASK MANAGER ===");
    Console.WriteLine("1. Add New Task");
    Console.WriteLine("2. View All Tasks");
    Console.WriteLine("3. Search Task by ID");
    Console.WriteLine("4. Update Task Status");
    Console.WriteLine("5. Delete Task");
    Console.WriteLine("6. Exit \n");

    Console.Write("Enter your choice: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            // Add New Task

            string? title;
            do
            {
                Console.Write("\nEnter Task Title: ");
                title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Task title cannot be empty.");

                }

            } while (string.IsNullOrWhiteSpace(title));

            Console.Write("\n");
            Console.Write("Enter Task Description: ");
            string? description = Console.ReadLine();
            

            string? priorityInput;

            do {
                Console.Write("\n");

                Console.WriteLine("Select Task Priority:");
                Console.WriteLine("1. Low");
                Console.WriteLine("2. Medium");
                Console.WriteLine("3. High");

                priorityInput = Console.ReadLine();

                // Validate user input
                if (priorityInput != "1" &&
                    priorityInput != "2" &&
                    priorityInput != "3")
                {
                    Console.WriteLine("Invalid priority. Please select 1, 2, or 3.");
                }

            } while (priorityInput != "1" &&
                    priorityInput != "2" &&
                    priorityInput != "3");

            // Convert valid input to enum
            TaskPriority priority = priorityInput switch
            {
                "1" => TaskPriority.Low,
                "2" => TaskPriority.Medium,
                "3" => TaskPriority.High,
                _ => throw new InvalidOperationException()
            };

            WorkTask newTask = new WorkTask
            {
                Title = title,
                Description = description ?? "No Description",
                Priority = priority
            };

           

            taskService.AddTask(newTask);
            Console.WriteLine($"\nTask '{newTask.Title}' added with ID: {newTask.Id}\n");
            break;

        case "2":
            // View All Tasks
            var tasks = taskService.GetAllTasks();
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.\n");
            }
            else
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Status: {task.Status}, Priority: {task.Priority}");
                }
                Console.WriteLine("\n");
            }
            break;

        case "3":
            // Search Task by ID
            Console.Write("\n");

            Console.Write("Enter Task ID: ");
            string? idInput = Console.ReadLine();
            if (int.TryParse(idInput, out int taskId))
            {
                var task = taskService.GetTaskById(taskId);
                if (task != null)
                {
                    Console.WriteLine($"\nID: {task.Id}, Title: {task.Title}, Status: {task.Status}, Priority: {task.Priority}");
                }
                else
                {
                    Console.WriteLine("\nTask not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid task ID.");
            }
            Console.WriteLine("\n");
            break;

        case "4":
            Console.Write("Enter Task ID to update: ");
            string? updateIdInput = Console.ReadLine();

            if (!int.TryParse(updateIdInput, out int updateTaskId))
            {
                Console.WriteLine("Invalid task ID.\n");
                break;
            }

            WorkTask? taskToUpdate = taskService.GetTaskById(updateTaskId);

            if (taskToUpdate == null)
            {
                Console.WriteLine("Task not found.\n");
                break;
            }

            Console.WriteLine("Select New Status:");
            Console.WriteLine("1. ToDo");
            Console.WriteLine("2. InProgress");
            Console.WriteLine("3. Done");
            Console.Write("\nEnter status: ");

            string? statusInput = Console.ReadLine();

            WorkTaskStatus newStatus;

            switch (statusInput)
            {
                case "1":
                    newStatus = WorkTaskStatus.ToDo;
                    break;

                case "2":
                    newStatus = WorkTaskStatus.InProgress;
                    break;

                case "3":
                    newStatus = WorkTaskStatus.Done;
                    break;

                default:
                    Console.WriteLine("Invalid status selection.\n");
                    continue;
            }

            bool isUpdated = taskService.UpdateTaskStatus(updateTaskId, newStatus);

            if (isUpdated)
            {
                Console.WriteLine($"\nTask ID {updateTaskId} status updated to {newStatus}.\n");
            }
            else
            {
                Console.WriteLine("\nTask update failed.");
            }

            break;

        case "5":
            // Delete Task
            Console.Write("Enter Task ID to delete: ");
            string? deleteIdInput = Console.ReadLine();
            if (int.TryParse(deleteIdInput, out int deleteTaskId))
            {
                bool isDeleted = taskService.DeleteTask(deleteTaskId);
                if (isDeleted)
                {
                    Console.WriteLine($"\nTask ID {deleteTaskId} deleted successfully.\n");
                }
                else
                {
                    Console.WriteLine("Task not found.\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid task ID.\n");
            }
            break;

        case "6":
            // Exit
            isRunning = false;
            Console.WriteLine("\nExiting the application. Goodbye!\n");
            break;

        default:
            Console.WriteLine("\nInvalid option. Please select 1 to 6.6\n");
            break;
    }

    
}