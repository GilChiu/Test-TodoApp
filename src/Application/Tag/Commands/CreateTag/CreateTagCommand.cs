using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Events;

namespace Todo_App.Application.Tag.Commands.CreateTag;
public record CreateTagCommand : IRequest<int>
{
    public int TodoItemId { get; init; }
    public string? Name { get; init; }
}

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Todo_App.Domain.Entities.Tag { Name = request.Name };
        _context.Tags.Add(tag);

        tag.AddDomainEvent(new TagCreatedEvent(tag));

        await _context.SaveChangesAsync(cancellationToken);
        return tag.Id;
    }
}

