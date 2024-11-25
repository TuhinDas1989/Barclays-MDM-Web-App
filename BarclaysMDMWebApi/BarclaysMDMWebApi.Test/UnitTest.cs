using BarclaysMDMWebApi.Controllers;
using BarclaysMDMWebApi.DataRepository;
using BarclaysMDMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BarclaysMDMWebApi.Test
{
    public class UnitTest
    {
        private readonly Mock<TaskRepo> _mockRepo;
        private readonly TaskController _controller;

        public UnitTest()
        {
            _mockRepo = new Mock<TaskRepo>();
            _controller = new TaskController(_mockRepo.Object);
        }

        [Fact]
        public void GetTasks_WithTaskList()
        {
            // Arrange
            var mockTasks = new List<TaskDTO>
            {
                new() { Id = 1, Name = "Task 1", Priority = 5, Status = "In Progress" },
                new() { Id = 2, Name = "Task 2", Priority = 9, Status = "Completed" }
            };

            // Act
            var result = _controller.GetTasks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<TaskDTO>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public void AddTask_WhenTaskIsValid()
        {
            // Arrange
            var newTask = new TaskDTO { Id = 3, Name = "New Task", Priority = 5, Status = "In Progress" };

            // Act
            var result = _controller.AddTask(newTask);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTasks", createdAtActionResult.ActionName);
            var returnValue = Assert.IsType<TaskDTO>(createdAtActionResult.Value);
            Assert.Equal("New Task", returnValue.Name);
        }

        [Fact]
        public void AddTask_WhenTaskNameIsNotUnique()
        {
            // Arrange
            var task1 = new TaskDTO { Id = 3, Name = "Task 1", Priority = 5, Status = "In Progress" };
            var task2 = new TaskDTO { Id = 5, Name = "Task 1", Priority = 10, Status = "Completed" };

            // Act
            _controller.AddTask(task1);
            var result = _controller.AddTask(task2);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Task name already present. Please try different name.", badRequestResult.Value);
        }

        [Fact]
        public void AddTask_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");
            var invalidTask = new TaskDTO { Id = 4, Name = "", Priority = 5, Status = "In Progress" };

            // Act
            var result = _controller.AddTask(invalidTask);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Task details.", badRequestResult.Value);
        }

        [Fact]
        public void EditTask_WhenTaskIsValid()
        {
            // Arrange
            var task = new TaskDTO { Id = 1, Name = "Test Task", Priority = 1, Status = "Not Started" };
            var updatedTask = new TaskDTO { Id = 1, Name = "Updated Task", Priority = 5, Status = "In Progress" };

            // Act
            _controller.AddTask(task);
            var result = _controller.EditTask(updatedTask);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TaskDTO>(okResult.Value);
            Assert.Equal("Updated Task", returnValue.Name);
        }

        [Fact]
        public void EditTask_WhenTaskDoesNotExist()
        {
            // Arrange
            var updatedTask = new TaskDTO { Id = 999, Name = "Nonexistent Task", Priority = 1, Status = "Not Started" };

            // Act
            var result = _controller.EditTask(updatedTask);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteTask_WhenTaskIsDeleted()
        {
            // Arrange
            int taskId = 1;

            // Act
            var result = _controller.DeleteTask(taskId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}