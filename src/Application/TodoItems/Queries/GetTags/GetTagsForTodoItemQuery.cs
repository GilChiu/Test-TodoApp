using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.Tag.Queries.GetTags;

namespace Todo_App.Application.TodoItems.Queries.GetTagsForTodoItem;

public record GetTagsForTodoItemQuery : IRequest<TagsVm>
{
    public int TodoItemId { get; init; }
}

public class GetTagsForTodoItemQueryHandler : IRequestHandler<GetTagsForTodoItemQuery, TagsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTagsForTodoItemQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TagsVm> Handle(GetTagsForTodoItemQuery request, CancellationToken cancellationToken)
    {
        var tags = await _context.TodoItems
            .Where(ti => ti.Id == request.TodoItemId)
            .SelectMany(ti => ti.Tags.Select(tt => tt.Name))
            .AsNoTracking()
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new TagsVm
        {
            Tags = tags
        };
    }
}
