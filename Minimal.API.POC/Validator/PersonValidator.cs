using FluentValidation;
using Minimal.API.POC.Model;

namespace Minimal.API.POC.Validator;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name is not null");
    }
}
