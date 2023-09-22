using System.Net.Http.Json;
using RPGClient.Models.Weapon;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class WeaponService : IWeaponService
{
    private readonly IUriBuilderService _uriBuilder;
    private readonly IAuthenticationService _authentication;
    private readonly HttpClient _httpClient;

    public WeaponService(IUriBuilderService uriBuilder, IAuthenticationService authentication, HttpClient httpClient)
    {
        _uriBuilder = uriBuilder;
        _authentication = authentication;
        _httpClient = httpClient;
    }

    public async Task AddWeapon(AddWeaponDto weaponDto)
    {
        await _authentication.Authenticate();
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Weapon.Weapon, weaponDto);
        res.EnsureSuccessStatusCode();
    }
}