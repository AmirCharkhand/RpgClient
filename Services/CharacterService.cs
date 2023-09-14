using System.Net.Http.Json;
using MudBlazor;
using RPGClient.Models;
using RPGClient.Models.Character;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class CharacterService : ICharacterService
{
    private readonly HttpClient _httpClient;

    public CharacterService(HttpClient httpClient, ISessionStorageService sessionStorageService)
    {
        _httpClient = httpClient;
    }
    
    public async Task<TableData<GetCharacterDto>> GetCharacters(TableMetaData tableMetaData)
    {
        var uri =
            $"/API/Character?PropertyName={tableMetaData.SortingPropName}&Ascending={tableMetaData.Ascending}&PageIndex={tableMetaData.PageIndex}&PageSize={tableMetaData.PageSize}";
        return await GetTableDataFromServer(uri);
    }
    
    public async Task<TableData<GetCharacterDto>> SearchCharacters(TableMetaData tableMetaData, string searchText)
    {
        var uri =
            $"/API/Character/{searchText}?PageIndex={tableMetaData.PageIndex}&PageSize={tableMetaData.PageSize}&PropertyName={tableMetaData.SortingPropName}&Ascending={tableMetaData.Ascending}";
        return await GetTableDataFromServer(uri);
    }

    public async Task AddCharacter(AddCharacterDto characterDto)
    {
        var res = await _httpClient.PostAsJsonAsync("/API/Character", characterDto);
        res.EnsureSuccessStatusCode();
    }

    public async Task UpdateCharacter(UpdateCharacterDto characterDto, int characterId)
    {
        var uri = $"/API/Character/{characterId}";
        var res = await _httpClient.PutAsJsonAsync(uri, characterDto);
        res.EnsureSuccessStatusCode();
    }

    public async Task DeleteCharacters(List<int> ids)
    {
        var res = await _httpClient.PostAsJsonAsync("/API/Character/GroupDelete", ids);
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