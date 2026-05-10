using Application.DTO.Student;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Students
{
    public class GetStudentByIdEndpoint : Endpoint<GetStudentByIdRequest, StudentResponse>
    {
        private readonly IStudentService _studentService;

        public GetStudentByIdEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Get("/api/students/{id}");
            Validator<GetStudentByIdValidator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetStudentByIdRequest req, CancellationToken ct)
        {
            var result = await _studentService.GetStudentByIdAsync(req.Id);
            await Send.OkAsync(result, cancellation: ct);
        }
    }
}
