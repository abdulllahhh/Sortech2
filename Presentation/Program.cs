using FastEndpoints;
using FastEndpoints.Swagger;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddSingleton(TimeProvider.System);

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            builder.Services
               .AddFastEndpoints()
               .SwaggerDocument(o =>                    // ← FastEndpoints.Swagger
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




            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseFastEndpoints()
                .UseAuthentication()
                .UseAuthorization()
                .UseSwaggerGen();

            app.MapControllers();

            app.Run();
        }
    }
}
