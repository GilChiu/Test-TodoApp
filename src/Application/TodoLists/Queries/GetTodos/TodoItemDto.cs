using AutoMapper;
using Todo_App.Application.Common.Mappings;
using Todo_App.Application.Tag.Queries.GetTags;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoLists.Queries.GetTodos;

public class TodoItemDto : IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }

    public int Priority { get; set; }

    public string? Note { get; set; }
    public string? Color { get; set; }
    public List<TagDto>? Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TodoItem, TodoItemDto>()
            .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority))
            .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.Tags.Select(tag => new TagDto
            {
                Id = tag.Id,
                Name = tag.Name,
                // Map other properties as needed
            })));
    }
}
