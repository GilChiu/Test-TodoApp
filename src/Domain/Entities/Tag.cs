using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Entities;
public class Tag : BaseAuditableEntity
{
    public string? Name { get; set; }

    public ICollection<TodoItemTags> TodoItemTags { get; set; } = new List<TodoItemTags>();
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

}
