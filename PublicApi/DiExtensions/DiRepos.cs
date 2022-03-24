using DataAccess.Repos;
using DataAccess.Repos.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PublicApi.DiExtensions
{
    static class DiRepos
    {
        public static IServiceCollection AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<ITeacherRepo, TeacherRepo>();
            services.AddScoped<IHomeWorkRepo, HomeWorkRepo>();
            services.AddScoped<IRollBookRepo, RollBookRepo>();
            services.AddScoped<ISubjectRepo, SubjectRepo>();

            return services;
        }
    }
}