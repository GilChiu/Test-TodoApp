using Microsoft.EntityFrameworkCore;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Todo_App.Domain.Entities.Tag> Tags { get; set; }
    DbSet<Todo_App.Domain.Entities.TodoItemTags> TodoItemTag { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
