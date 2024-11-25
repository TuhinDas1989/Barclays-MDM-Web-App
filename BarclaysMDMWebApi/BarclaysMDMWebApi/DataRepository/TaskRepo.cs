using BarclaysMDMWebApi.Models;
using System.Threading.Tasks;

namespace BarclaysMDMWebApi.DataRepository
{
    public class TaskRepo
    {
        private readonly List<TaskDTO> taskList = new();

        public List<TaskDTO> GetTasks()
        {
            try
            {
                return taskList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TaskDTO GetTaskById(int id)
        {
            try
            {
                return taskList.FirstOrDefault(t => t.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TaskDTO AddTask(TaskDTO task)
        {
            try
            {
                task.Id = taskList.Count + 1;
                taskList.Add(task);
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TaskDTO EditTask(TaskDTO task)
        {
            try
            {
                var existingTask = taskList.FirstOrDefault(t => t.Id == task.Id);
                if (existingTask != null)
                {
                    existingTask.Name = task.Name;
                    existingTask.Priority = task.Priority;
                    existingTask.Status = task.Status;
                }
                return existingTask;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteTask(int id)
        {
            try
            {
                var task = taskList.FirstOrDefault(t => t.Id == id && t.Status?.ToUpper() == "COMPLETED");
                if (task != null)
                {
                    taskList.Remove(task);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsTaskNameUnique(int taskId, string taskName)
        {
            try
            {
                return !taskList.Any(t => t.Name?.ToUpper() == taskName.ToUpper() && t.Id != taskId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
