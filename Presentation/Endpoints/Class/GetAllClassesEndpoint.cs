using Application.DTO.Class;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Class
{
    public class GetAllClassesEndpoint : Endpoint<GetAllClassesRequest, IEnumerable<ClassResponse>>
    {
        private readonly IClassService _classService;

        public GetAllClassesEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Get("/api/classes");
            Validator<GetAllClasseValidator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllClassesRequest req, CancellationToken ct)
        {
            var result = await _classService.GetAllClassesAsync(req.Page, req.PageSize, req.Name, req.Teacher);
            await Send.OkAsync(result, cancellation: ct);
        }
    }
}
