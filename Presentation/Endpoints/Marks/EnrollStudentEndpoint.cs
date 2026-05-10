using Application.DTO.Enrollment;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;
using Infrastructure.Services;

namespace Presentation.Endpoints.Marks
{
    public class EnrollStudentEndpoint : Endpoint<EnrollStudentRequest, EnrolmentResponse>
    {
        private readonly IMarkService _markService;

        public EnrollStudentEndpoint(IMarkService markService)
        {
            _markService = markService;
        }

        public override void Configure()
        {
            Post("/api/enrollments");
            AllowAnonymous();
            Validator<EnrollStudentValidator>();
        }

        public override async Task HandleAsync(EnrollStudentRequest req, CancellationToken ct)
        {
            var result = await _markService.EnrollStudentAsync(req);
            await Send.OkAsync(result, cancellation: ct);
        }
    }
}
