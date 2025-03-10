using MediatR;
using SimpleKeyValueStore.Interfaces;

namespace SimpleKeyValueStore.Middleware;

public class ExclusiveCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest
{
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not IExclusiveCommand)
        {
            return await next();
        }
        
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return await next();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}