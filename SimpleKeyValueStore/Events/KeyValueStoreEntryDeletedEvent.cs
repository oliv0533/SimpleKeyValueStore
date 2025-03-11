using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace SimpleKeyValueStore.Events;

public record KeyValueStoreEntryDeletedEvent(string Key) : INotification;

public class KeyValueStoreEntryDeletedEventNotificationHandler : INotificationHandler<KeyValueStoreEntryDeletedEvent>
{
    private readonly IMemoryCache _memoryCache;

    public KeyValueStoreEntryDeletedEventNotificationHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public Task Handle(KeyValueStoreEntryDeletedEvent notification, CancellationToken cancellationToken)
    {
        _memoryCache.Remove(notification.Key);
        return Task.CompletedTask;
    }
}