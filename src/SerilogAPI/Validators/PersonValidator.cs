using FluentValidation;
using SerilogAPI.Entities;

namespace SerilogAPI.Validators;

public sealed class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.Age).GreaterThan(0);

        RuleFor(p => p.Name).Length(3, 100);
    }
}
