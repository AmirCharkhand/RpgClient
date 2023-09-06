using System.Net.Http.Json;
using RPGClient.Models;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class LoginService : ILoginService
{
    private readonly HttpClient _httpClient;

    public LoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Login(UserLoginDto userLogin)
    {
        var res = await _httpClient.PostAsJsonAsync("/API/Auth/login", userLogin);
        res.EnsureSuccessStatusCode();
        var result = await res.Content.ReadAsStringAsync();
        return result;
    }
}