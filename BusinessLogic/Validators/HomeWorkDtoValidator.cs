using BusinessLogic.ModelsDto;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class HomeWorkDtoValidator : AbstractValidator<HomeWorkDto>
    {
        public HomeWorkDtoValidator()
        {
            RuleFor(homeWork => homeWork.Grade).InclusiveBetween(0, 5);
            RuleFor(homeWork => homeWork.SubjectId).GreaterThanOrEqualTo(0);
            RuleFor(homeWork => homeWork.StudentId).GreaterThanOrEqualTo(0);
        }
    }
}