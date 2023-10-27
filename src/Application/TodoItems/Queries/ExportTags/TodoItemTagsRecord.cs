using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_App.Application.Common.Mappings;

namespace Todo_App.Application.TodoItems.Queries.ExportTags;
public class TodoItemTagsRecord : IMapFrom<Todo_App.Domain.Entities.TodoItemTags>
{
    public int TodoItemId { get; set; }
    public int TagId { get; set; }
}
