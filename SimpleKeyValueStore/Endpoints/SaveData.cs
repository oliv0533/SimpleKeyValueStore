using FastEndpoints;
using MediatR;
using SimpleKeyValueStore.Interfaces;
using SimpleKeyValueStore.UseCases.Commands;

namespace SimpleKeyValueStore.Endpoints;

public class SaveData : Endpoint<SaveDataRequest>
{
    private readonly IMediator _mediator;

    public SaveData(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Post(Constants.Endpoints.SaveData);
        AllowAnonymous();
    }

    public override async Task HandleAsync(SaveDataRequest req, CancellationToken ct)
    {
        await _mediator.Send(new SaveDataCommand(req.Key, req.Value), ct);

        await SendOkAsync(ct);
    }
}

public record SaveDataRequest(string Key, object Value);