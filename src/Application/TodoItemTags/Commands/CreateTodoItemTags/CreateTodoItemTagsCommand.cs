using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Todo_App.Application.Common.Interfaces;

namespace Todo_App.Application.TodoItemTags.Commands.CreateTodoItemTags;
public record CreateTodoItemTagsCommand : IRequest
{
    public int TodoItemId { get; init; }
    public int TagId { get; init; }
}

public class CreateTodoItemTagsCommandHandler : IRequestHandler<CreateTodoItemTagsCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemTagsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateTodoItemTagsCommand request, CancellationToken cancellationToken)
    {
        var todoItemTags = new Domain.Entities.TodoItemTags
        {
            TodoItemId = request.TodoItemId,
            TagId = request.TagId
        };
        _context.TodoItemTag.Add(todoItemTags);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}