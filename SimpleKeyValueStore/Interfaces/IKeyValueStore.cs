namespace SimpleKeyValueStore.Interfaces;

public interface IKeyValueStore
{
    Task SaveDataAsync(string key, object value, CancellationToken cancellationToken);
    
    Task<object?> GetDataAsync(string key, CancellationToken cancellationToken);
    
    Task DeleteDataAsync(string key, CancellationToken cancellationToken);
}