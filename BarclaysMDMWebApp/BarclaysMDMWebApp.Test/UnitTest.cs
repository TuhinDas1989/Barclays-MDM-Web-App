using BarclaysMDMWebApp.Controllers;
using BarclaysMDMWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BarclaysMDMWebApp.Test
{
    public class UnitTest
    {
        private readonly TaskController _controller;

        public UnitTest()
        {
            _controller = new TaskController();
        }

        [Fact]
        public void AddEdit_TaskList()
        {
            // Arrange
            int? taskId = null;

            // Act
            var result = _controller.List();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void AddEdit_GetNewTaskDetails()
        {
            // Arrange
            int? taskId = null;

            // Act
            var result = _controller.AddEdit(taskId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void AddEdit_GetModifyTaskDetails()
        {
            // Arrange
            var result = _controller.AddEdit(1);

            // Act
            var viewResult = Assert.IsType<ViewResult>(result);

            // Assert
            Assert.True(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void AddEdit_AddNewTask()
        {
            // Arrange
            var task = new TaskDTO { Name = "Test Task Name", Priority = 1, Status = "Not Started" };

            // Act
            var result = (RedirectToActionResult)_controller.AddEdit(task);

            // Assert
            Assert.Equal("List", result.ActionName);
        }

        [Fact]
        public void AddEdit_AddDuplicateTask()
        {
            // Arrange
            var task1 = new TaskDTO { Name = "Test Same Task Name", Priority = 1, Status = "Not Started" };
            var task2 = new TaskDTO { Name = "Test Same Task Name", Priority = 5, Status = "In Progress" };

            // Act
            _controller.AddEdit(task1);
            var result = _controller.AddEdit(task2);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void AddEdit_ModifyTask()
        {
            // Arrange
            var task = new TaskDTO { Name = "Test Task Name", Priority = 1, Status = "Not Started" };

            // Act
            var result1 = (RedirectToActionResult)_controller.AddEdit(task);

            // Assert
            Assert.Equal("List", result1.ActionName);

            // Arrange
            task.Id = 1;
            task.Name = "Test Task Name - Update";

            // Act
            var result2 = (RedirectToActionResult)_controller.AddEdit(task);

            // Assert
            Assert.Equal("List", result2.ActionName);
        }

        [Fact]
        public void DeleteTask()
        {
            // Arrange
            int taskId = 1;

            // Act
            var result = (RedirectToActionResult)_controller.Delete(taskId);

            // Assert
            Assert.Equal("List", result.ActionName);
        }
    }
}