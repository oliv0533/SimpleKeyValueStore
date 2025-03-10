using FastEndpoints;
using MediatR;
using SimpleKeyValueStore.Interfaces;
using SimpleKeyValueStore.UseCases.Commands;

namespace SimpleKeyValueStore.Endpoints;

public class DeleteData : Endpoint<DeleteDataRequest>
{
    private readonly IMediator _mediator;

    public DeleteData(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Delete(Constants.Endpoints.DeleteData);
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteDataRequest req, CancellationToken ct)
    {
        await _mediator.Send(new DeleteDataCommand(req.Key), ct);
        
        await SendOkAsync(ct);
    }
}

public class DeleteDataRequest
{
    [QueryParam]
    public string Key { get; set; } = string.Empty;
}