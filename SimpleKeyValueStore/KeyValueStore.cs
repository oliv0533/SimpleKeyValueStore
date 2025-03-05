using System.Text;
using System.Text.Json;
using SimpleKeyValueStore.Interfaces;

namespace SimpleKeyValueStore;

public class KeyValueStore : IKeyValueStore
{
    private readonly string _filePath;

    public KeyValueStore(IConfiguration configuration)
    {
        _filePath = configuration.GetValue<string>("KeyValueStore:FilePath") 
                    ?? throw new ApplicationException("Path for KeyValueStore is missing.");

        if (!File.Exists(_filePath))
        {
            var jsonString = JsonSerializer.Serialize(new Dictionary<string, object>(), new JsonSerializerOptions { WriteIndented = true });
            
            File.WriteAllText(_filePath, jsonString);
        }
    }
    
    public async Task SaveDataAsync (string key, object value, CancellationToken cancellationToken)
    {
        try
        {
            await using FileStream fileStream = new(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            
            var dictionary = await GetDictionaryFromJsonAsync(fileStream, cancellationToken);

            dictionary[key] = value;
            
            await SaveDictionaryToFileSystem(dictionary, fileStream, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<object?> GetDataAsync(string key, CancellationToken cancellationToken)
    {
        try
        {
            await using FileStream fileStream = new(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            var dictionary = await GetDictionaryFromJsonAsync(fileStream, cancellationToken);
            
            return dictionary.GetValueOrDefault(key);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteDataAsync(string key, CancellationToken cancellationToken)
    {
        try
        {
            await using FileStream fileStream = new(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            
            var dictionary = await GetDictionaryFromJsonAsync(fileStream, cancellationToken);
            
            dictionary.Remove(key);
            
            await SaveDictionaryToFileSystem(dictionary, fileStream, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private static async Task<Dictionary<string, object>> GetDictionaryFromJsonAsync(
        FileStream fileStream,
        CancellationToken cancellationToken)
    {
        var dictionary = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(fileStream, cancellationToken: cancellationToken);

        if (dictionary is null)
        {
            throw new ApplicationException("File not found.");
        }

        return dictionary;
    }
    
    private static async Task SaveDictionaryToFileSystem(Dictionary<string, object> dictionary,
        FileStream fileStream, CancellationToken cancellationToken)
    {
        var jsonString = JsonSerializer.Serialize(dictionary);
            
        var jsonBytes = Encoding.UTF8.GetBytes(jsonString);
        var jsonMemory = new ReadOnlyMemory<byte>(jsonBytes);

        fileStream.SetLength(0);
        fileStream.Seek(0, SeekOrigin.Begin);

        await fileStream.WriteAsync(jsonMemory, cancellationToken);
    }
}