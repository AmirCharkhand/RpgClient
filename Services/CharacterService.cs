using System.Net.Http.Json;
using MudBlazor;
using RPGClient.Models;
using RPGClient.Models.Character;
using RPGClient.Models.Skill;
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
    
    public async Task<TableData<GetOwnedCharacterDto>> GetOwnedCharacters(PagedListParameters parameters)
    {
        await _authenticationService.Authenticate();
        var uri = _uriBuilder.Character.GetUriForOwnedCharacters(parameters);
        return await GetTableDataFromServer<GetOwnedCharacterDto>(uri);
    }

    public async Task<TableData<GetUniversalCharacterDto>> GetUniversalCharacters(PagedListParameters parameters)
    {
        await _authenticationService.Authenticate();
        var uri = _uriBuilder.Character.GetUriForUniversalCharacters(parameters);
        return await GetTableDataFromServer<GetUniversalCharacterDto>(uri);
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

    public async Task<List<GetSkillDto>> AddCharacterSkill(CharacterSkillDto characterSkill)
    {
        await _authenticationService.Authenticate();
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Character.GetUriForAddSkill, characterSkill);
        res.EnsureSuccessStatusCode();
        var result = await res.Content.ReadFromJsonAsync<List<GetSkillDto>>();
        return result!;
    }

    public async Task<List<GetSkillDto>> RemoveCharacterSkill(CharacterSkillDto characterSkill)
    {
        await _authenticationService.Authenticate();
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Character.GetUriForRemoveSkill, characterSkill);
        res.EnsureSuccessStatusCode();
        var result = await res.Content.ReadFromJsonAsync<List<GetSkillDto>>();
        return result!;
    }

    private async Task<TableData<T>> GetTableDataFromServer<T>(string uri)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, uri);
        var res = await _httpClient.SendAsync(req);
        res.EnsureSuccessStatusCode();
        var characters = await res.Content.ReadFromJsonAsync<List<T>>();
        var totalItems = int.Parse(res.Headers.GetValues("X_TotalCount").First());
        return new TableData<T>
        {
            Items = characters,
            TotalItems = totalItems
        };
    }
}