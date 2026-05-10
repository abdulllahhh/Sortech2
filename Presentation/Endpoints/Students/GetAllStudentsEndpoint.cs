using Application.DTO.Student;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Students
{
    public class GetAllStudentsEndpoint : Endpoint<GetAllStudentsRequest, IEnumerable<StudentResponse>>
    {
        private readonly IStudentService _studentService;

        public GetAllStudentsEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Get("/api/students");
            Validator<GetAllStudentsValidator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllStudentsRequest req, CancellationToken ct)
        {
            var result = await _studentService.GetAllStudentsAsync(req.Page, req.PageSize, req.Name, req.Age);
            await Send.OkAsync(result, cancellation: ct);
        }
    }
}
