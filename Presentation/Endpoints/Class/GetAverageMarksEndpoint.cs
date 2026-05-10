using Application.DTO.Class;
using Application.Interfaces.Services;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Class
{
    public class GetAverageMarksEndpoint : Endpoint<GetAverageMarksRequest, object>
    {
        private readonly IClassService _classService;

        public GetAverageMarksEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Get("/api/classes/{classId}/average-marks");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAverageMarksRequest req, CancellationToken ct)
        {
            var result = await _classService.GetAverageMarksAsync(req.ClassId);
            await Send.OkAsync(new { classId = req.ClassId, averageMark = result }, cancellation: ct);
        }
    }
}
