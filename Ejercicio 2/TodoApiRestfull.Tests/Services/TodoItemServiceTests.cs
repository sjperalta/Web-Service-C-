using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TodoApiRestfull.Data;
using TodoApiRestfull.Models;
using TodoApiRestfull.Services;
using Xunit;

namespace TodoApiRestfull.Tests.Services
{
    public class TodoItemServiceTests
    {
        private readonly TodoService _service;
        private readonly Mock<TodoContext> _mockContext;

        public TodoItemServiceTests()
        {
            _mockContext = new Mock<TodoContext>();
            _mockContext.SetupGet(x => x.TodoItems).ReturnsDbSet(GenerateItems);        
            _service = new TodoService(_mockContext.Object);
        }

        private IList<TodoItem> GenerateItems =>
            new List<TodoItem>
            {
                new TodoItem { Id = 1, Name = "Task 1", IsComplete = false },
                new TodoItem { Id = 2, Name = "Task 2", IsComplete = true },
                new TodoItem { Id = 3, Name = "Task 2 test", IsComplete = true }
            };

        [Fact]
        public async Task GetTodoItemsAsync_should_return_all_items()
        {
            // Arrange
            // Act
            var result = await _service.GetTodoItemsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Task 1", result.First().Name);
        }

        [Fact]
        public async Task GetTodoItemAsync_should_returns_correct_item()
        {
            // Arrange
            // Act
            var result = await _service.GetTodoItemAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Task 1", result.Name);
        }

        [Fact]
        public async Task CreateTodoItemAsync_should_adds_new_item()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "Test_CreateTodoItem")
                .Options;

            using (var context = new TodoContext(options))
            {
                var service = new TodoService(context);
                var newItem = new TodoItem { Id = 3, Name = "New Task", IsComplete = false };

                // Act
                var result = await service.CreateTodoItemAsync(newItem);

                // Assert
                Assert.Equal(3, result.Id);
                Assert.Equal("New Task", result.Name);
            }

            // Verify item was added
            using (var context = new TodoContext(options))
            {
                Assert.Equal(1, context.TodoItems.Count());
                Assert.Equal("New Task", context.TodoItems.Single().Name);
            }
        }
    }
}