using Application.DTO.StudentReport;
using Application.Interfaces.Services;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Marks
{
    public class GetStudentReportEndpoint : Endpoint<GetReportRequest, StudentReportResponse>
    {
        private readonly IMarkService _markService;

        public GetStudentReportEndpoint(IMarkService markService)
        {
            _markService = markService;
        }

        public override void Configure()
        {
            Get("/api/students/{studentId}/report");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetReportRequest req, CancellationToken ct)
        {
            var result = await _markService.GetStudentReportAsync(req.StudentId);
            await Send.OkAsync(result, cancellation: ct);
        }
    }
}
