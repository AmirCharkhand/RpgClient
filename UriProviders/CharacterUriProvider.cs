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

    public string GetUriForPagedCharacters(PagedListParameters parameters)
    {
        var uri = Character;
        if (!string.IsNullOrEmpty(parameters.SearchText))
            uri += $"/{parameters.SearchText}";
        
        uri += $"?PageIndex={parameters.PageIndex}&PageSize={parameters.PageSize}";
        
        if (!string.IsNullOrEmpty(parameters.SortingPropName))
            uri += $"&PropertyName={parameters.SortingPropName}&Ascending={parameters.Ascending}";
        
        return uri;
    }

    public string GetUriForUpdate(int characterId)
    {
        return Character + $"/{characterId}";
    }
}