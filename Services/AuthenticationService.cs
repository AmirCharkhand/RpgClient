using System.Net.Http.Headers;
using System.Net.Http.Json;
using RPGClient.Models;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly IUriBuilderService _uriBuilder;
    private readonly ISessionStorageService _storageService;

    public AuthenticationService(HttpClient httpClient, IUriBuilderService uriBuilder, ISessionStorageService storageService)
    {
        _httpClient = httpClient;
        _uriBuilder = uriBuilder;
        _storageService = storageService;
    }

    public async Task Login(UserLoginDto userLogin)
    {
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Authentication.GetUriForLogin, userLogin);
        res.EnsureSuccessStatusCode();
        var result = await res.Content.ReadAsStringAsync();
        await _storageService.AddItem("JWT", result);
        await SetAuthenticationHeader(result);
    }

    public async Task Authenticate()
    {
        if (_httpClient.DefaultRequestHeaders.Authorization != null) return;
        await SetAuthenticationHeader();
    }

    private async Task SetAuthenticationHeader(string bearer = "")
    {
        if (string.IsNullOrEmpty(bearer)) 
            bearer = await _storageService.GetItem<string>("JWT") ?? throw new InvalidOperationException("Not Logged in");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearer);
    }
}