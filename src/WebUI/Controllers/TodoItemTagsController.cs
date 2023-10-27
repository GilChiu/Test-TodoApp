using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.Tag.Commands.AddRemoveTags;

namespace Todo_App.WebUI.Controllers;


[Route("api/TodoItemTags")]
public class TodoItemTagsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoItemTagsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddRemoveTags")]
    public async Task<ActionResult> AddRemoveTags(AddRemoveTagsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}