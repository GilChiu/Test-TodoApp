using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Todo_App.Application.Common.Mappings;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoItemTags.Queries.GetTodoItemTags;
public class TodoItemTagDto : IMapFrom<Todo_App.Domain.Entities.TodoItemTags>
{
    public int Id { get; set; }
    public int TodoItemId { get; set; }
    public int TagId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Todo_App.Domain.Entities.TodoItemTags, TodoItemTagDto>();
    }
}
