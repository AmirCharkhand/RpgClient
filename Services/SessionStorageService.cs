using System.Text.Json;
using Microsoft.JSInterop;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class SessionStorageService : ISessionStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public SessionStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T?> GetItem<T>(string key)
    {
        var json = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", key);
        return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json);
    }

    public async Task AddItem<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, json);
    }

    public async Task RemoveItem(string key)
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
    }
}