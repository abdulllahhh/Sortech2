using Application.DTO.Student;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Students
{
    public class CreateStudentEndpoint : Endpoint<CreateStudentRequest, StudentResponse>
    {
        private readonly IStudentService _studentService;

        public CreateStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Post("/api/students");
            AllowAnonymous();
            Validator<CreateStudentValidator>();
            Summary(s =>
            {
                s.Summary = "Create a new student";
                s.Description = "Validates and stores a new student in the in-memory store.";
            });
        }

        public override async Task HandleAsync(CreateStudentRequest req, CancellationToken ct)
        {
            var result = await _studentService.CreateStudentAsync(req);
            await Send.CreatedAtAsync<GetStudentByIdEndpoint>(new { id = result.Id }, result, cancellation: ct);
        }
    }
}
