using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.Common.Models;
using Todo_App.Application.Tag.Commands.CreateTag;
using Todo_App.Application.Tag.Commands.DeleteTag;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Todo_App.WebUI.Controllers;

public class TagController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTagCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTagCommand(id));
        return NoContent();
    }
}

