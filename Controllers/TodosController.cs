using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(ITodoService todoService) : ControllerBase
{
    // GET api/todos
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetAll() => Ok(todoService.GetAll());

    // GET api/todos/5
    [HttpGet("{id:int}")]
    public ActionResult<TodoItem> GetById(int id)
    {
        var item = todoService.GetById(id);
        return item is null ? NotFound() : Ok(item);
    }

    // POST api/todos
    [HttpPost]
    public ActionResult<TodoItem> Create(CreateTodoDto dto)
    {
        var item = todoService.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    // PUT api/todos/5
    [HttpPut("{id:int}")]
    public ActionResult<TodoItem> Update(int id, UpdateTodoDto dto)
    {
        var item = todoService.Update(id, dto);
        return item is null ? NotFound() : Ok(item);
    }

    // DELETE api/todos/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) => todoService.Delete(id) ? NoContent() : NotFound();
}
