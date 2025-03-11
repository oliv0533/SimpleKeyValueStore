using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace SimpleKeyValueStore.Events;

public record KeyValueStoreEntryModifiedEvent(string Key, object? Value) : INotification;

public class KeyValueStoreEntryModifiedEventNotificationHandler : INotificationHandler<KeyValueStoreEntryModifiedEvent>
{
    private readonly IMemoryCache _memoryCache;

    public KeyValueStoreEntryModifiedEventNotificationHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task Handle(KeyValueStoreEntryModifiedEvent notification, CancellationToken cancellationToken)
    {
        _memoryCache.Set(notification.Key, notification.Value);
    }
}