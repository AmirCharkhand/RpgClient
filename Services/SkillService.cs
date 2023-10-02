using System.Net.Http.Json;
using System.Text.Json;
using RPGClient.Models.Skill;
using RPGClient.Services.Contracts;

namespace RPGClient.Services;

public class SkillService : ISkillService
{
    private readonly IUriBuilderService _uriBuilder;
    private readonly HttpClient _httpClient;

    public SkillService(IUriBuilderService uriBuilder, HttpClient httpClient)
    {
        _uriBuilder = uriBuilder;
        _httpClient = httpClient;
    }

    public async Task<List<GetSkillDto>?> GetAll()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _uriBuilder.Skill.Skill);
        var result = await GetSkillsFromServer(request);
        return result;
    }
    
    public async Task<List<GetSkillDto>?> GetACharacterSkills(int characterId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _uriBuilder.Skill.GetUriForSkillsOfACharacter(characterId));
        var result = await GetSkillsFromServer(request);
        return result;
    }
    
    private async Task<List<GetSkillDto>?> GetSkillsFromServer(HttpRequestMessage request)
    {
        var res = await _httpClient.SendAsync(request);
        res.EnsureSuccessStatusCode();
        var result = await res.Content.ReadFromJsonAsync<List<GetSkillDto>>();
        return result;
    }
}