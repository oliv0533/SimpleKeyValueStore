using MediatR;
using SimpleKeyValueStore.Interfaces;

namespace SimpleKeyValueStore.UseCases.Queries;

public record GetDataQuery(string Key) : IRequest<object?>;

public class GetDataQueryHandler : IRequestHandler<GetDataQuery, object?>
{
    private readonly IKeyValueStore _keyValueStore;

    public GetDataQueryHandler(IKeyValueStore keyValueStore)
    {
        _keyValueStore = keyValueStore;
    }
    
    public async Task<object?> Handle(GetDataQuery request, CancellationToken cancellationToken)
    {
        return await _keyValueStore.GetDataAsync(request.Key, cancellationToken);
    }
}