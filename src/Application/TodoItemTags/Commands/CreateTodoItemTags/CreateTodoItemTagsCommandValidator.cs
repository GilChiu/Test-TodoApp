using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace Todo_App.Application.TodoItemTags.Commands.CreateTodoItemTags;
public class CreateTodoItemTagsCommandValidator : AbstractValidator<CreateTodoItemTagsCommand>
{
    public CreateTodoItemTagsCommandValidator()
    {
        RuleFor(v => v.TodoItemId)
            .NotEmpty().WithMessage("TodoItemId is required.");

        RuleFor(v => v.TagId)
            .NotEmpty().WithMessage("TagId is required.");
    }
}
