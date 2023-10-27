using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Enums;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }
    public int ListId { get; init; }
    public PriorityLevel Priority { get; init; }
    public string? Note { get; init; }
    public string? Color { get; init; }
    public List<int>? TagIds { get; init; } // Add a list of tag IDs
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .Include(ti => ti.Tags) // Include the Tags navigation property
            .FirstOrDefaultAsync(ti => ti.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;
        entity.Color = request.Color;
        // Handle tags (this part will depend on how your tags are associated with TodoItems)

        // Update tags associated with the TodoItem
        entity.Tags.Clear();
        if (request.TagIds != null && request.TagIds.Any())
        {
            entity.Tags = request.TagIds
                .Select(tagId => new Tag { Id = tagId }) // Create a new Tag entity with the selected tag IDs
                .ToList();
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}
