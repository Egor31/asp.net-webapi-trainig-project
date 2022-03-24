using BusinessLogic;
using BusinessLogic.MessageServices;
using BusinessLogic.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PublicApi.DiExtensions
{
    static class DiServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRollBookService, RollBookService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IHomeWorkService, HomeWorkService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISmsService, SmsService>();
            return services;
        }
    }
}