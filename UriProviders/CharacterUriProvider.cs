using RPGClient.Models;

namespace RPGClient.UriProviders;

public class CharacterUriProvider
{
    public string Character { get; }

    public CharacterUriProvider()
    {
        Character = "API/Character";
    }
    
    public string GetUriForGroupDelete => Character + "/GroupDelete";

    public string GetUriForAddSkill => Character + "/AddSkill";
    public string GetUriForRemoveSkill => Character + "/RemoveSkill";

    public string GetUriForOwnedCharacters(PagedListParameters parameters) =>
        GetUriForPagedCharacters(parameters, Character);

    public string GetUriForUniversalCharacters(PagedListParameters parameters) =>
        GetUriForPagedCharacters(parameters, Character + "/Universal");

    private string GetUriForPagedCharacters(PagedListParameters parameters, string baseUri)
    {
        if (!string.IsNullOrEmpty(parameters.SearchText))
            baseUri += $"/{parameters.SearchText}";
        
        baseUri += $"?PageIndex={parameters.PageIndex}&PageSize={parameters.PageSize}";
        
        if (!string.IsNullOrEmpty(parameters.SortingPropName))
            baseUri += $"&PropertyName={parameters.SortingPropName}&Ascending={parameters.Ascending}";
        
        return baseUri;
    }

    public string GetUriForUpdate(int characterId)
    {
        return Character + $"/{characterId}";
    }
}