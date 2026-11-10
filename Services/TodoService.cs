using System.Collections.Concurrent;
using WebApi.Models;

namespace WebApi.Services;

public interface ITodoService
{
    IEnumerable<TodoItem> GetAll();
    TodoItem? GetById(int id);
    TodoItem Create(CreateTodoDto dto);
    TodoItem? Update(int id, UpdateTodoDto dto);
    bool Delete(int id);
}

/// <summary>Einfache In-Memory-Implementierung (Daten gehen beim Neustart verloren).</summary>
public class InMemoryTodoService : ITodoService
{
    private readonly ConcurrentDictionary<int, TodoItem> _items = new();
    private int _nextId;

    public IEnumerable<TodoItem> GetAll() => _items.Values.OrderBy(t => t.Id);

    public TodoItem? GetById(int id) => _items.GetValueOrDefault(id);

    public TodoItem Create(CreateTodoDto dto)
    {
        var item = new TodoItem
        {
            Id = Interlocked.Increment(ref _nextId),
            Title = dto.Title
        };
        _items[item.Id] = item;
        return item;
    }

    public TodoItem? Update(int id, UpdateTodoDto dto)
    {
        if (!_items.TryGetValue(id, out var item)) return null;
        item.Title = dto.Title;
        item.IsDone = dto.IsDone;
        return item;
    }

    public bool Delete(int id) => _items.TryRemove(id, out _);
}
