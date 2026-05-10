using Application.DTO.Class;
using Application.DTO.Mark;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Class
{
    public class GetAverageMarksEndpoint : Endpoint<GetAverageMarksRequest, AverageMarksResponse>
    {
        private readonly IClassService _classService;

        public GetAverageMarksEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Get("/api/classes/{classId}/average-marks");
            Validator<GetAverageMarksValidator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAverageMarksRequest req, CancellationToken ct)
        {
            var result = await _classService.GetAverageMarksAsync(req.ClassId);
            await Send.OkAsync(new AverageMarksResponse { ClassId = req.ClassId, AvgMark = result }, cancellation: ct);
        }
    }
}
