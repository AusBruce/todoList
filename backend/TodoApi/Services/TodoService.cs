using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(int id);
        TodoItem Create(TodoItem todoItem);
        TodoItem? Update(int id, TodoItem todoItem);
        bool Delete(int id);
    }

    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> _todoItems = new();
        private int _nextId = 1;

        public TodoService()
        {
            // Add some sample data
            _todoItems.Add(new TodoItem { Id = _nextId++, Title = "Learn Angular", IsCompleted = false, CreatedAt = DateTime.UtcNow.AddDays(-2) });
            _todoItems.Add(new TodoItem { Id = _nextId++, Title = "Build .NET Web API", IsCompleted = true, CreatedAt = DateTime.UtcNow.AddDays(-1) });
            _todoItems.Add(new TodoItem { Id = _nextId++, Title = "Create TODO App", IsCompleted = false, CreatedAt = DateTime.UtcNow });
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todoItems.OrderByDescending(x => x.CreatedAt);
        }

        public TodoItem? GetById(int id)
        {
            return _todoItems.FirstOrDefault(x => x.Id == id);
        }

        public TodoItem Create(TodoItem todoItem)
        {
            todoItem.Id = _nextId++;
            todoItem.CreatedAt = DateTime.UtcNow;
            _todoItems.Add(todoItem);
            return todoItem;
        }

        public TodoItem? Update(int id, TodoItem todoItem)
        {
            var existingItem = _todoItems.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
                return null;

            existingItem.Title = todoItem.Title;
            existingItem.IsCompleted = todoItem.IsCompleted;
            
            return existingItem;
        }

        public bool Delete(int id)
        {
            var item = _todoItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return false;

            return _todoItems.Remove(item);
        }
    }
} 