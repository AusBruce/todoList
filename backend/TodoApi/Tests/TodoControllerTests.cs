using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests
{
    public class TodoControllerTests
    {
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly TodoController _controller;

        public TodoControllerTests()
        {
            _mockTodoService = new Mock<ITodoService>();
            _controller = new TodoController(_mockTodoService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOkResultWithTodos()
        {
            // Arrange
            var expectedTodos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Test 1", IsCompleted = false, CreatedAt = DateTime.UtcNow },
                new TodoItem { Id = 2, Title = "Test 2", IsCompleted = true, CreatedAt = DateTime.UtcNow }
            };
            _mockTodoService.Setup(x => x.GetAll()).Returns(expectedTodos);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTodos = Assert.IsType<List<TodoItem>>(okResult.Value);
            Assert.Equal(expectedTodos.Count, returnedTodos.Count);
        }

        [Fact]
        public void GetById_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var expectedTodo = new TodoItem { Id = 1, Title = "Test Todo", IsCompleted = false, CreatedAt = DateTime.UtcNow };
            _mockTodoService.Setup(x => x.GetById(1)).Returns(expectedTodo);

            // Act
            var result = _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTodo = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal(expectedTodo.Id, returnedTodo.Id);
        }

        [Fact]
        public void GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockTodoService.Setup(x => x.GetById(999)).Returns((TodoItem?)null);

            // Act
            var result = _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_WithValidTodo_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var newTodo = new TodoItem { Title = "New Todo", IsCompleted = false };
            var createdTodo = new TodoItem { Id = 1, Title = "New Todo", IsCompleted = false, CreatedAt = DateTime.UtcNow };
            _mockTodoService.Setup(x => x.Create(It.IsAny<TodoItem>())).Returns(createdTodo);

            // Act
            var result = _controller.Create(newTodo);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedTodo = Assert.IsType<TodoItem>(createdAtActionResult.Value);
            Assert.Equal(createdTodo.Id, returnedTodo.Id);
            Assert.Equal("GetById", createdAtActionResult.ActionName);
        }

        [Fact]
        public void Create_WithInvalidModel_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidTodo = new TodoItem { Title = "", IsCompleted = false };
            _controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = _controller.Create(invalidTodo);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Update_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var updatedTodo = new TodoItem { Title = "Updated Todo", IsCompleted = true };
            var returnedTodo = new TodoItem { Id = 1, Title = "Updated Todo", IsCompleted = true, CreatedAt = DateTime.UtcNow };
            _mockTodoService.Setup(x => x.Update(1, It.IsAny<TodoItem>())).Returns(returnedTodo);

            // Act
            var result = _controller.Update(1, updatedTodo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal(returnedTodo.Id, returned.Id);
        }

        [Fact]
        public void Update_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var updatedTodo = new TodoItem { Title = "Updated Todo", IsCompleted = true };
            _mockTodoService.Setup(x => x.Update(999, It.IsAny<TodoItem>())).Returns((TodoItem?)null);

            // Act
            var result = _controller.Update(999, updatedTodo);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Update_WithInvalidModel_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidTodo = new TodoItem { Title = "", IsCompleted = false };
            _controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = _controller.Update(1, invalidTodo);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Delete_WithValidId_ShouldReturnNoContent()
        {
            // Arrange
            _mockTodoService.Setup(x => x.Delete(1)).Returns(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockTodoService.Setup(x => x.Delete(999)).Returns(false);

            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ToggleComplete_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var existingTodo = new TodoItem { Id = 1, Title = "Test Todo", IsCompleted = false, CreatedAt = DateTime.UtcNow };
            var toggledTodo = new TodoItem { Id = 1, Title = "Test Todo", IsCompleted = true, CreatedAt = DateTime.UtcNow };
            _mockTodoService.Setup(x => x.GetById(1)).Returns(existingTodo);
            _mockTodoService.Setup(x => x.Update(1, It.IsAny<TodoItem>())).Returns(toggledTodo);

            // Act
            var result = _controller.ToggleComplete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<TodoItem>(okResult.Value);
            Assert.True(returned.IsCompleted);
        }

        [Fact]
        public void ToggleComplete_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockTodoService.Setup(x => x.GetById(999)).Returns((TodoItem?)null);

            // Act
            var result = _controller.ToggleComplete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
} 