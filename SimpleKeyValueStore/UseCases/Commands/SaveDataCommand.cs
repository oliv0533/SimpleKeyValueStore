using MediatR;
using SimpleKeyValueStore.Interfaces;

namespace SimpleKeyValueStore.UseCases.Commands;

public record SaveDataCommand(string Key, object Value) : IRequest, IExclusiveCommand;


public class SaveDataCommandHandler : IRequestHandler<SaveDataCommand>
{
    private readonly IKeyValueStore _keyValueStore;

    public SaveDataCommandHandler(IKeyValueStore keyValueStore)
    {
        _keyValueStore = keyValueStore;
    }
    
    public async Task Handle(SaveDataCommand request, CancellationToken cancellationToken)
    {
        await _keyValueStore.SaveDataAsync(request.Key, request.Value, cancellationToken);
    }
}