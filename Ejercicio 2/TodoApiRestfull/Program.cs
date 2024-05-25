using Microsoft.EntityFrameworkCore;
using TodoApiRestfull.Data;
using TodoApiRestfull.Services;
using TodoApiRestfull.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//AddControllers es una funcion que permite utilizar controllers para mapear las llamadas
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

//Funcion que se utiliza para mapear los controllers
app.MapControllers();

app.Run();
