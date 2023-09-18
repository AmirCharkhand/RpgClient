using System.Net.Http.Json;
using MudBlazor;
using RPGClient.Models;
using RPGClient.Models.Character;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class CharacterService : ICharacterService
{
    private readonly HttpClient _httpClient;
    private readonly IUriBuilderService _uriBuilder;
    private readonly IAuthenticationService _authenticationService;

    public CharacterService(HttpClient httpClient, IUriBuilderService uriBuilder, IAuthenticationService authenticationService)
    {
        _httpClient = httpClient;
        _uriBuilder = uriBuilder;
        _authenticationService = authenticationService;
    }
    
    public async Task<TableData<GetCharacterDto>> GetCharacters(PagedListParameters parameters)
    {
        await _authenticationService.Authenticate();
        var uri = _uriBuilder.Character.GetUriForPagedCharacters(parameters);
        return await GetTableDataFromServer(uri);
    }
    
    public async Task AddCharacter(AddCharacterDto characterDto)
    {
        await _authenticationService.Authenticate();
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Character.Character, characterDto);
        res.EnsureSuccessStatusCode();
    }

    public async Task UpdateCharacter(UpdateCharacterDto characterDto, int characterId)
    {
        await _authenticationService.Authenticate();
        var uri = _uriBuilder.Character.GetUriForUpdate(characterId);
        var res = await _httpClient.PutAsJsonAsync(uri, characterDto);
        res.EnsureSuccessStatusCode();
    }

    public async Task DeleteCharacters(List<int> ids)
    {
        await _authenticationService.Authenticate();
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Character.GetUriForGroupDelete, ids);
        res.EnsureSuccessStatusCode();
    }

    private async Task<TableData<GetCharacterDto>> GetTableDataFromServer(string uri)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, uri);
        var res = await _httpClient.SendAsync(req);
        res.EnsureSuccessStatusCode();
        var characters = await res.Content.ReadFromJsonAsync<List<GetCharacterDto>>();
        var totalItems = int.Parse(res.Headers.GetValues("X_TotalCount").First());
        return new TableData<GetCharacterDto>
        {
            Items = characters,
            TotalItems = totalItems
        };
    }
}