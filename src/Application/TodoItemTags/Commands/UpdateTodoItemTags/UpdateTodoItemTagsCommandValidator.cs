using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Todo_App.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Todo_App.Application.TodoItemTags.Commands.UpdateTodoItemTags;

public class UpdateTodoItemTagsCommandValidator : AbstractValidator<UpdateTodoItemTagsCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemTagsCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.TodoItemId)
            .NotEmpty().WithMessage("TodoItemId is required.")
            .MustAsync(BeValidTodoItemId).WithMessage("Invalid TodoItemId.");

        RuleFor(v => v.TagIds)
            .NotEmpty().WithMessage("TagId is required.")
            .MustAsync((v, tagIds, ct) => BeUnique(v.TodoItemId, tagIds, ct))
            .WithMessage("Invalid TagId or duplicate combination.");
    }

    public async Task<bool> BeValidTodoItemId(int todoItemId, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .AnyAsync(ti => ti.Id == todoItemId, cancellationToken);
    }

    public async Task<bool> BeUnique(int todoItemId, List<int> tagIds, CancellationToken cancellationToken)
    {
        // Check if there is any duplicate combination of TodoItemId and TagId
        return !await _context.TodoItemTag
            .AnyAsync(tt => tt.TodoItemId == todoItemId && tagIds.Contains(tt.TagId), cancellationToken);
    }
}
