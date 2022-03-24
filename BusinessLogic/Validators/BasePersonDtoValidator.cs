using BusinessLogic.ModelsDto;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public abstract class BasePersonDtoValidator : AbstractValidator<BasePersonDto>
    {
        public BasePersonDtoValidator()
        {
            RuleFor(person => person.FirstName).MaximumLength(50).NotNull();
            RuleFor(person => person.LastName).MaximumLength(50).NotNull();
            RuleFor(person => person.Email).EmailAddress().NotNull();
            RuleFor(person => person.PhoneNumber).MaximumLength(20).NotNull();
        }
    }
}