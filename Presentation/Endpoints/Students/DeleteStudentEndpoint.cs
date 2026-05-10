using Application.DTO.Student;
using Application.Interfaces.Services;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Students
{
    public class DeleteStudentEndpoint : Endpoint<DeleteStudentRequest>
    {
        private readonly IStudentService _studentService;

        public DeleteStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Delete("/api/students/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteStudentRequest req, CancellationToken ct)
        {
            await _studentService.DeleteStudentAsync(req.Id);
            await Send.NoContentAsync(ct);
        }
    }
}
