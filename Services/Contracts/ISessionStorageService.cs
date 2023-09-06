namespace RPGClient.Services.Contracts;

public interface ISessionStorageService
{
    Task<T?> GetItem<T>(string key);
    Task AddItem<T>(string key, T value);
    Task RemoveItem(string key);
}