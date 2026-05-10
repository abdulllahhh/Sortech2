using Application.DTO.Class;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;
using Infrastructure.Services;

namespace Presentation.Endpoints.Class
{
    public class CreateClassEndpoint : Endpoint<CreateClassRequest, ClassResponse>
    {
        private readonly IClassService _classService;

        public CreateClassEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Post("/api/classes");
            AllowAnonymous();
            Validator<CreateClassValidator>();
        }

        public override async Task HandleAsync(CreateClassRequest req, CancellationToken ct)
        {
            var result = await _classService.CreateClassAsync(req);
            await Send.OkAsync(result, cancellation: ct);
        }
    }
}
