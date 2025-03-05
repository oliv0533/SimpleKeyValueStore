using FastEndpoints;
using SimpleKeyValueStore.Interfaces;

namespace SimpleKeyValueStore.Endpoints;

public class GetData : Endpoint<GetDataRequest, GetDataResponse>
{
    private readonly IKeyValueStore _keyValueStore;

    public GetData(IKeyValueStore keyValueStore)
    {
        _keyValueStore = keyValueStore;
    }
    
    public override void Configure()
    {
        Get(Constants.Endpoints.GetData);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDataRequest req, CancellationToken ct)
    {
        var value = await _keyValueStore.GetDataAsync(req.Key, ct);

        await SendAsync(new GetDataResponse(value), cancellation: ct);
    }
}

public record GetDataRequest([property: QueryParam]string Key);

public record GetDataResponse(object? Value);