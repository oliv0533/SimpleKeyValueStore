using MediatR;
using SimpleKeyValueStore.Interfaces;

namespace SimpleKeyValueStore.UseCases.Commands;

public record DeleteDataCommand(string Key) : IRequest, IExclusiveCommand;

public class DeleteDataCommandHandler : IRequestHandler<DeleteDataCommand>
{
    private readonly IKeyValueStore _keyValueStore;

    public DeleteDataCommandHandler(IKeyValueStore keyValueStore)
    {
        _keyValueStore = keyValueStore;
    }
    
    public async Task Handle(DeleteDataCommand request, CancellationToken cancellationToken)
    {
        await _keyValueStore.DeleteDataAsync(request.Key, cancellationToken);
    }
}