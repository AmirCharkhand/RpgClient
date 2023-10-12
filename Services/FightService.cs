using System.Net.Http.Json;
using RPGClient.Models.Fight;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class FightService : IFightService
{
    private readonly IUriBuilderService _uriBuilder;
    private readonly HttpClient _httpClient;

    public FightService(IUriBuilderService uriBuilder, HttpClient httpClient)
    {
        _uriBuilder = uriBuilder;
        _httpClient = httpClient;
    }

    public async Task<List<string>> Fight(List<int> ids)
    {
        var dto = new FightRequestDto() { CharacterIds = ids };
        var res = await _httpClient.PostAsJsonAsync(_uriBuilder.Fight.Fight, dto);
        res.EnsureSuccessStatusCode();
        var result = await res.Content.ReadFromJsonAsync<FightResultDto>();
        return result!.Log;
    }
}