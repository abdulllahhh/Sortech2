using Application.DTO.Student;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Students
{
    public class UpdateStudentEndpoint : Endpoint<UpdateStudentRequest>
    {
        private readonly IStudentService _studentService;

        public UpdateStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Put("/api/students/{id}");
            AllowAnonymous();
            Validator<UpdateStudentValidator>();
        }

        public override async Task HandleAsync(UpdateStudentRequest req, CancellationToken ct)
        {
            await _studentService.UpdateStudentAsync(req.Id, req);
            await Send.NoContentAsync(ct);
        }
    }
}
