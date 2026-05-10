using Application.DTO;
using FastEndpoints;

namespace Presentation.Controllers.Endpoints
{
    public class WeatherForcastEndpoint : Endpoint<MyRequest, MyResponse>
    {
        public override void Configure()
        {
            Post("/hello/world");
            AllowAnonymous();
        }
        public override async Task HandleAsync(MyRequest r, CancellationToken c)
        {
            await Send.ResponseAsync(new()
            {
                FullName = $"{r.FirstName} {r.LastName}",
                Message = "Welcome to FastEndpoints..."
            });
        }
    }
}
