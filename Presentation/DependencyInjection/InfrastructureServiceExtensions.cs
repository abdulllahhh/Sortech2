using Application.Interfaces.Repository;
using Application.Services.Repository;
using Domain.Models;
using Infrastructure.Data;

namespace Presentation.DependencyInjection
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton(InMemoryDataStore.Students);
            services.AddSingleton(InMemoryDataStore.Classes);
            services.AddSingleton(InMemoryDataStore.Enrolments);
            services.AddSingleton(InMemoryDataStore.Marks);

            services.AddScoped<IRepository<Student>, Repository<Student>>(sp =>
                new Repository<Student>(sp.GetRequiredService<System.Collections.Concurrent.ConcurrentDictionary<int, Student>>()));

            services.AddScoped<IRepository<Class>, Repository<Class>>(sp =>
                new Repository<Class>(sp.GetRequiredService<System.Collections.Concurrent.ConcurrentDictionary<int, Class>>()));

            services.AddScoped<IRepository<Enrollment>, Repository<Enrollment>>(sp =>
                new Repository<Enrollment>(sp.GetRequiredService<System.Collections.Concurrent.ConcurrentDictionary<int, Enrollment>>()));

            services.AddScoped<IRepository<Mark>, Repository<Mark>>(sp =>
                new Repository<Mark>(sp.GetRequiredService<System.Collections.Concurrent.ConcurrentDictionary<int, Mark>>()));

            return services;
        }
    }
}
