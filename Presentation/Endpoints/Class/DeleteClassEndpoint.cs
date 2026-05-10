using Application.DTO.Class;
using Application.Interfaces.Services;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Class
{
    public class DeleteClassEndpoint : Endpoint<DeleteClassRequest>
    {
        private readonly IClassService _classService;

        public DeleteClassEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Delete("/api/classes/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteClassRequest req, CancellationToken ct)
        {
            await _classService.DeleteClassAsync(req.Id);
            await Send.NoContentAsync(ct);
        }
    }
}
