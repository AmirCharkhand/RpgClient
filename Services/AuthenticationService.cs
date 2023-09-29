using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RPGClient.Models;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly IUriBuilderService _uriBuilder;
    private readonly ISessionStorageService _storageService;
    private readonly NavigationManager _navigationManager;

    public AuthenticationService(HttpClient httpClient, IUriBuilderService uriBuilder, ISessionStorageService storageService, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _uriBuilder = uriBuilder;
        _storageService = storageService;
        _navigationManager = navigationManager;
    }

    public async Task Login(UserLoginDto userLogin)
    {
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Authentication.GetUriForLogin, userLogin);
        res.EnsureSuccessStatusCode();
        var result = await res.Content.ReadFromJsonAsync<AuthDto>() ?? throw new NullReferenceException();
        await _storageService.AddItem("JWT", result.Jwt);
        await _storageService.AddItem("UserName", result.UserName);
        await SetAuthenticationHeader(result.Jwt);
    }

    public async Task Authenticate()
    {
        if (_httpClient.DefaultRequestHeaders.Authorization != null) return;
        await SetAuthenticationHeader();
    }

    public async Task Logout()
    {
        await _storageService.RemoveItem("JWT");
        await _storageService.RemoveItem("UserName");
        _httpClient.DefaultRequestHeaders.Clear();
        _navigationManager.NavigateTo("/");
    }

    private async Task SetAuthenticationHeader(string bearer = "")
    {
        if (string.IsNullOrEmpty(bearer)) 
            bearer = await _storageService.GetItem<string>("JWT") ?? throw new InvalidOperationException("Not Logged in");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearer);
    }
}