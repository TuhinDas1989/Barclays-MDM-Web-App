using BarclaysMDMWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarclaysMDMWebApp.Controllers
{
    public class TaskController : Controller
    {
        private static List<TaskDTO> taskList = [];
        private static int _nextId = 1;

        public IActionResult List()
        {
            try
            {
                return View(taskList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult AddEdit(int? id)
        {
            try
            {
                TaskDTO task = new();
                if (id.HasValue)
                {
                    task = taskList.FirstOrDefault(t => t.Id == id);
                }
                return View(task);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddEdit(TaskDTO task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (taskList.Any(t => t.Name?.ToUpper() == task.Name?.ToUpper() && t.Id != task.Id))
                    {
                        ModelState.AddModelError("Name", "Task name already present. Please try different name.");
                        return View(task);
                    }

                    if (task.Id == 0)
                    {
                        task.Id = _nextId++;
                        taskList.Add(task);
                    }
                    else
                    {
                        var existingTask = taskList.FirstOrDefault(t => t.Id == task.Id);
                        if (existingTask != null)
                        {
                            existingTask.Name = task.Name;
                            existingTask.Priority = task.Priority;
                            existingTask.Status = task.Status;
                        }
                    }
                    return RedirectToAction(nameof(List));
                }
                return View(task);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var task = taskList.FirstOrDefault(t => t.Id == id && t.Status?.ToUpper() == "COMPLETED");
                if (task != null)
                {
                    taskList.Remove(task);
                }
                return RedirectToAction(nameof(List));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
