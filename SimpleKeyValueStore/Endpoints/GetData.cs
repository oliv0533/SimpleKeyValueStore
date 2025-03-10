using FastEndpoints;
using MediatR;
using SimpleKeyValueStore.Interfaces;
using SimpleKeyValueStore.UseCases.Queries;

namespace SimpleKeyValueStore.Endpoints;

public class GetData : Endpoint<GetDataRequest, GetDataResponse>
{
    private readonly IMediator _mediator;

    public GetData(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Get(Constants.Endpoints.GetData);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDataRequest req, CancellationToken ct)
    {
        var value = await _mediator.Send(new GetDataQuery(req.Key), ct);

        await SendAsync(new GetDataResponse(value), cancellation: ct);
    }
}

public record GetDataRequest([property: QueryParam]string Key);

public record GetDataResponse(object? Value);