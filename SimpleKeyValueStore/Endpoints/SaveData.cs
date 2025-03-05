using FastEndpoints;
using SimpleKeyValueStore.Interfaces;

namespace SimpleKeyValueStore.Endpoints;

public class SaveData : Endpoint<SaveDataRequest>
{
    private readonly IKeyValueStore _keyValueStore;

    public SaveData(IKeyValueStore keyValueStore)
    {
        _keyValueStore = keyValueStore;
    }
    
    public override void Configure()
    {
        Post(Constants.Endpoints.SaveData);
        AllowAnonymous();
    }

    public override async Task HandleAsync(SaveDataRequest req, CancellationToken ct)
    {
        await _keyValueStore.SaveDataAsync(req.Key, req.Value, ct);

        await SendOkAsync(ct);
    }
}

public record SaveDataRequest(string Key, object Value);