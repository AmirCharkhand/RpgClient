namespace RPGClient.UriProviders;

public class SkillUriProvider
{
    public string Skill { get; }

    public SkillUriProvider()
    {
        Skill = "API/Skill";
    }

    public string GetUriForSkillsOfACharacter(int characterId) => $"{Skill}/Skills of {characterId}";
}