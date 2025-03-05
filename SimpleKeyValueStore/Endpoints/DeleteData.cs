using FastEndpoints;
using SimpleKeyValueStore.Interfaces;

namespace SimpleKeyValueStore.Endpoints;

public class DeleteData : Endpoint<DeleteDataRequest>
{
    private readonly IKeyValueStore _keyValueStore;

    public DeleteData(IKeyValueStore keyValueStore)
    {
        _keyValueStore = keyValueStore;
    }
    
    public override void Configure()
    {
        Delete(Constants.Endpoints.DeleteData);
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteDataRequest req, CancellationToken ct)
    {
        await _keyValueStore.DeleteDataAsync(req.Key, ct);
        
        await SendOkAsync(ct);
    }
}

public class DeleteDataRequest
{
    [QueryParam]
    public string Key { get; set; } = string.Empty;
}