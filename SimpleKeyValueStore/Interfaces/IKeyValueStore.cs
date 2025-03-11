namespace SimpleKeyValueStore.Interfaces;

/// <summary>
/// This is an interface for a key-value store with some basic operations
/// </summary>
public interface IKeyValueStore
{
    /// <summary>
    /// Saves the data with the given key
    /// </summary>
    /// <param name="key">The key used to save the data in the key-value store</param>
    /// <param name="value">The value saved in the data</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Task</returns>
    Task SaveDataAsync(string key, object value, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves the data with the given key
    /// </summary>
    /// <param name="key">They key used to query the key-value store</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>The object, if it exists. If not, null is returned</returns>
    Task<object?> GetDataAsync(string key, CancellationToken cancellationToken);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteDataAsync(string key, CancellationToken cancellationToken);
}