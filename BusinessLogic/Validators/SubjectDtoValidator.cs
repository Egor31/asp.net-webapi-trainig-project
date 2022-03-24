using BusinessLogic.ModelsDto;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SubjectDtoValidator : AbstractValidator<SubjectDto>
    {
        public SubjectDtoValidator()
        {
            RuleFor(subject => subject.Title).NotNull();
            RuleFor(subject => subject.TeacherId).GreaterThanOrEqualTo(0);
        }
    }
}