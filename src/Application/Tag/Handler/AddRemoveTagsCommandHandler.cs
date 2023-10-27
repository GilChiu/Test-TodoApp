using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.Tag.Commands.AddRemoveTags;

namespace Todo_App.Application.Tag.Handler;
public class AddRemoveTagsCommandHandler : IRequestHandler<AddRemoveTagsCommand>
{
    private readonly IApplicationDbContext _context;

    public AddRemoveTagsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddRemoveTagsCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the TodoItem by its ID
        var todoItem = await _context.TodoItems.FindAsync(request.TodoItemId);

        if (todoItem == null)
        {
            throw new NotFoundException("TodoItem", request.TodoItemId);
        }

        // Get the list of Tag IDs to be added
        var tagsToAdd = request.TagIdsToAdd;

        // Get the list of Tag IDs to be removed
        var tagsToRemove = request.TagIdsToRemove;

        // Load the current Tags related to the TodoItem
        var currentTags = todoItem.Tags.ToList();

        // Add new tags
        foreach (var tagId in tagsToAdd)
        {
            // Check if the tag is not already associated with the TodoItem
            if (!currentTags.Any(tag => tag.Id == tagId))
            {
                var tag = await _context.Tags.FindAsync(tagId);
                if (tag != null)
                {
                    todoItem.Tags.Add(tag);
                }
            }
        }

        // Remove tags
        foreach (var tagId in tagsToRemove)
        {
            var tagToRemove = currentTags.FirstOrDefault(tag => tag.Id == tagId);
            if (tagToRemove != null)
            {
                todoItem.Tags.Remove(tagToRemove);
            }
        }

        // Save changes to the database
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
