using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(_todoService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var todoItem = _todoService.GetById(id);
            if (todoItem == null)
                return NotFound();

            return Ok(todoItem);
        }

        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdItem = _todoService.Create(todoItem);
            return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public ActionResult<TodoItem> Update(int id, TodoItem todoItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedItem = _todoService.Update(id, todoItem);
            if (updatedItem == null)
                return NotFound();

            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = _todoService.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/toggle")]
        public ActionResult<TodoItem> ToggleComplete(int id)
        {
            var todoItem = _todoService.GetById(id);
            if (todoItem == null)
                return NotFound();

            todoItem.IsCompleted = !todoItem.IsCompleted;
            var updatedItem = _todoService.Update(id, todoItem);
            
            return Ok(updatedItem);
        }
    }
} 