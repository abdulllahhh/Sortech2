using Application.Interfaces.Services;
using Application.Validators;
using FluentValidation;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.DependencyInjection
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateStudentValidator).Assembly);

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IMarkService, MarkService>();

            return services;
        }
    }
}
