using Application.DependencyInjection;
using FastEndpoints;
using FastEndpoints.Swagger;
using Presentation.DependencyInjection;
using Presentation.Middleware;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddSingleton(TimeProvider.System);

            builder.Services.AddAuthorization();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.AddControllers();

            builder.Services
               .AddFastEndpoints()
               .SwaggerDocument(o =>
               {
                   o.DocumentSettings = s =>
                   {
                       s.Title = "My App API";
                       s.Version = "v1";
                   };
               });

            builder.Services.AddAuthorization();


            builder.Services.AddDataProtection();






            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseFastEndpoints()
                .UseSwaggerGen();

            app.MapControllers();

            app.Run();
        }
    }
}
