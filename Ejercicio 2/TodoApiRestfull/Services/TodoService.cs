using Microsoft.EntityFrameworkCore;
using TodoApiRestfull.Data;
using TodoApiRestfull.Models;
using TodoApiRestfull.Services.Interfaces;

namespace TodoApiRestfull.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            var result = await _context.TodoItems
                .Where(x => x.Id == id)
                .FirstAsync();

            if(result == null)
            {
                throw new Exception("Esto es un error porque no existe el registro en la base de datos");
            }

            return result;
        }

        public async Task<bool> UpdateTodoItemAsync(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return false;
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task<bool> DeleteTodoItemAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        public async Task<List<TodoItem>> Search(string name)
        {
            //retornara uno o mas elementos
            //select * from dbo.TodoItems t where t.name like '%name%' 
            var result = await _context.TodoItems
                .Where(item => item.Name.Contains(name))
                .ToListAsync();

            return result;
        }
    }
}