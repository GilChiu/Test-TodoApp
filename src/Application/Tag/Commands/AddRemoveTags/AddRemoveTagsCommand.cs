using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Todo_App.Application.Tag.Commands.AddRemoveTags;
public class AddRemoveTagsCommand : IRequest
{
    public int TodoItemId { get; set; }
    public List<int>? TagIdsToAdd { get; set; }
    public List<int>? TagIdsToRemove { get; set; }
}
