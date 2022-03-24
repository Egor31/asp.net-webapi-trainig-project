using BusinessLogic.ModelsDto;
using BusinessLogic.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace PublicApi.DiExtensions
{
    static class DiValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<TeacherDto>, TeacherDtoValidator>();
            services.AddScoped<IValidator<StudentDto>, StudentDtoValidator>();
            services.AddScoped<IValidator<HomeWorkDto>, HomeWorkDtoValidator>();
            services.AddScoped<IValidator<SubjectDto>, SubjectDtoValidator>();
            return services;
        }
    }
}