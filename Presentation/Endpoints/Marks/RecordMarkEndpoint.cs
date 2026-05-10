using Application.DTO.Mark;
using Application.Interfaces.Services;
using Application.Validators;
using FastEndpoints;
using Infrastructure.Data;

namespace Presentation.Endpoints.Marks
{
    public class RecordMarkEndpoint : Endpoint<RecordMarkRequest, MarkResponse>
    {
        private readonly IMarkService _markService;

        public RecordMarkEndpoint(IMarkService markService)
        {
            _markService = markService;
        }

        public override void Configure()
        {
            Post("/api/marks");
            AllowAnonymous();
            Validator<RecordMarkValidator>();
        }

        public override async Task HandleAsync(RecordMarkRequest req, CancellationToken ct)
        {
            var result = await _markService.RecordMarkAsync(req);
            await Send.OkAsync(result, cancellation: ct);
        }
    }
}
