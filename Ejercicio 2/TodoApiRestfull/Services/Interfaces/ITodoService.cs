
using TodoApiRestfull.Models;

namespace TodoApiRestfull.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemAsync(long id);
        Task<bool> UpdateTodoItemAsync(long id, TodoItem todoItem);
        Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);
        Task<bool> DeleteTodoItemAsync(long id);
        Task<List<TodoItem>> Search(string name);
        bool TodoItemExists(long id);
    }
}