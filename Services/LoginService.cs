using System.Net.Http.Headers;
using System.Net.Http.Json;
using RPGClient.Models;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class LoginService : ILoginService
{
    private readonly HttpClient _httpClient;
    private readonly IUriBuilderService _uriBuilder;

    public LoginService(HttpClient httpClient, IUriBuilderService uriBuilder)
    {
        _httpClient = httpClient;
        _uriBuilder = uriBuilder;
    }

    public async Task Login(UserLoginDto userLogin)
    {
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Authentication.GetUriForLogin, userLogin);
        res.EnsureSuccessStatusCode();
        var result = await res.Content.ReadAsStringAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result);
    }
}