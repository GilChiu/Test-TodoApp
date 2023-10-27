using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace Todo_App.Application.Tag.Commands.CreateTag;
public class CreateTagCommandValidator : AbstractValidator <CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}
