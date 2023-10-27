using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Todo_App.Application.Common.Interfaces;

namespace Todo_App.Application.TodoItemTags.Commands.DeleteTodoItemTags;
public record DeleteTodoItemTagsCommand(int TodoItemId, int TagId) : IRequest;
public class DeleteTodoItemTagsCommandHandler : IRequestHandler<DeleteTodoItemTagsCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemTagsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemTagsCommand request, CancellationToken cancellationToken)
    {
        // Find the TodoItemTags entity to delete based on the TodoItemId and TagId
        var todoItemTags = await _context.TodoItemTag.FindAsync(request.TodoItemId, request.TagId);

        if (todoItemTags != null)
        {
            _context.TodoItemTag.Remove(todoItemTags);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
