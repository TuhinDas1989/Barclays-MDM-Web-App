using BarclaysMDMWebApi.DataRepository;
using BarclaysMDMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarclaysMDMWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly TaskRepo _taskRepo;

        public TaskController(TaskRepo taskRepo)
        {
            _taskRepo = taskRepo;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            try
            {
                return Ok(_taskRepo.GetTasks());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] TaskDTO task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_taskRepo.IsTaskNameUnique(task.Id, task.Name))
                    {
                        return BadRequest("Task name already present. Please try different name.");
                    }

                    var addedTask = _taskRepo.AddTask(task);
                    return CreatedAtAction(nameof(GetTasks), new { id = addedTask.Id }, addedTask);
                }
                else
                {
                    return BadRequest("Invalid Task details.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut()]
        public IActionResult EditTask([FromBody] TaskDTO task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_taskRepo.IsTaskNameUnique(task.Id, task.Name))
                    {
                        return BadRequest("Task name already present. Please try different name.");
                    }

                    var existingTask = _taskRepo.GetTaskById(task.Id);
                    if (existingTask == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var updatedTask = _taskRepo.EditTask(task);
                        return Ok(updatedTask);
                    }
                }
                else
                {
                    return BadRequest("Invalid Task details.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                var deleted = _taskRepo.DeleteTask(id);
                return deleted ? Ok() : NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
