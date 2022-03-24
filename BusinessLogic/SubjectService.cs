using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using DataAccess.Models;
using DataAccess.Repos.Interfaces;
using FluentValidation;

namespace BusinessLogic
{
    public class SubjectService : BaseService<Subject, SubjectDto>, ISubjectService
    {
        public SubjectService(ISubjectRepo subjectRepo, IValidator<SubjectDto> subjectValidator) : 
            base(subjectRepo, subjectValidator)
        {
            
        }
    }
}