using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Hello World! testing2");

//endpopint: localhost:8282/todoitems
app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.ToListAsync());

// sql: select * from dbo.todos
app.Run();
