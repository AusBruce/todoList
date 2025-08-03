using Xunit;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Tests
{
    public class TodoServiceTests
    {
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _todoService = new TodoService();
        }

        [Fact]
        public void GetAll_ShouldReturnAllTodos()
        {
            // Act
            var todos = _todoService.GetAll();

            // Assert
            Assert.NotNull(todos);
            Assert.True(todos.Count() >= 3); // Should have at least 3 sample items
        }

        [Fact]
        public void GetAll_ShouldReturnTodosOrderedByCreatedAtDescending()
        {
            // Act
            var todos = _todoService.GetAll().ToList();

            // Assert
            Assert.True(todos.Count >= 2);
            for (int i = 0; i < todos.Count - 1; i++)
            {
                Assert.True(todos[i].CreatedAt >= todos[i + 1].CreatedAt);
            }
        }

        [Fact]
        public void GetById_WithValidId_ShouldReturnTodo()
        {
            // Arrange
            var allTodos = _todoService.GetAll().ToList();
            var expectedTodo = allTodos.First();

            // Act
            var result = _todoService.GetById(expectedTodo.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTodo.Id, result.Id);
            Assert.Equal(expectedTodo.Title, result.Title);
            Assert.Equal(expectedTodo.IsCompleted, result.IsCompleted);
        }

        [Fact]
        public void GetById_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidId = 999;

            // Act
            var result = _todoService.GetById(invalidId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Create_WithValidTodo_ShouldCreateAndReturnTodo()
        {
            // Arrange
            var newTodo = new TodoItem
            {
                Title = "Test Todo",
                IsCompleted = false
            };

            // Act
            var result = _todoService.Create(newTodo);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal("Test Todo", result.Title);
            Assert.False(result.IsCompleted);
            Assert.True(result.CreatedAt > DateTime.UtcNow.AddMinutes(-1));
        }

        [Fact]
        public void Create_ShouldIncrementId()
        {
            // Arrange
            var todo1 = new TodoItem { Title = "Todo 1", IsCompleted = false };
            var todo2 = new TodoItem { Title = "Todo 2", IsCompleted = false };

            // Act
            var result1 = _todoService.Create(todo1);
            var result2 = _todoService.Create(todo2);

            // Assert
            Assert.True(result2.Id > result1.Id);
        }

        [Fact]
        public void Update_WithValidId_ShouldUpdateAndReturnTodo()
        {
            // Arrange
            var allTodos = _todoService.GetAll().ToList();
            var existingTodo = allTodos.First();
            var updatedTodo = new TodoItem
            {
                Title = "Updated Todo",
                IsCompleted = true
            };

            // Act
            var result = _todoService.Update(existingTodo.Id, updatedTodo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingTodo.Id, result.Id);
            Assert.Equal("Updated Todo", result.Title);
            Assert.True(result.IsCompleted);
        }

        [Fact]
        public void Update_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidId = 999;
            var updatedTodo = new TodoItem
            {
                Title = "Updated Todo",
                IsCompleted = true
            };

            // Act
            var result = _todoService.Update(invalidId, updatedTodo);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Delete_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var allTodos = _todoService.GetAll().ToList();
            var todoToDelete = allTodos.First();

            // Act
            var result = _todoService.Delete(todoToDelete.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var invalidId = 999;

            // Act
            var result = _todoService.Delete(invalidId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Delete_ShouldRemoveTodoFromList()
        {
            // Arrange
            var newTodo = new TodoItem { Title = "Todo to Delete", IsCompleted = false };
            var createdTodo = _todoService.Create(newTodo);
            var initialCount = _todoService.GetAll().Count();

            // Act
            var deleteResult = _todoService.Delete(createdTodo.Id);
            var finalCount = _todoService.GetAll().Count();

            // Assert
            Assert.True(deleteResult);
            Assert.Equal(initialCount - 1, finalCount);
        }
    }
} 