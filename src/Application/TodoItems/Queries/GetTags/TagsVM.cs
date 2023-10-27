using Todo_App.Application.Tag.Queries.GetTags;

namespace Todo_App.Application.TodoItems.Queries.GetTagsForTodoItem;

public class TagsVm
{
    public IList<TagDto> Tags { get; set; } = new List<TagDto>();
}