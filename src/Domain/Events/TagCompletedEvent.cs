using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Events;
public class TagCompletedEvent : BaseEvent
{
    public TagCompletedEvent(Tag item)
    {
        Item = item;
    }

    public Tag Item { get; }
}
