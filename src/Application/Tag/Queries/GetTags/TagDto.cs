using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Todo_App.Application.Common.Mappings;

namespace Todo_App.Application.Tag.Queries.GetTags;
public class TagDto : IMapFrom<Domain.Entities.Tag>
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Tag, TagDto>();
    }
}
