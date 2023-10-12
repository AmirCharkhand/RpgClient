using RPGClient.UriProviders;

namespace RPGClient.Services.Contracts;

public interface IUriBuilderService
{
    public string BaseUri { get; init; }
    public AuthenticationUriProvider Authentication { get; }
    public CharacterUriProvider Character { get; }
    public WeaponUriProvider Weapon { get; }
    public SkillUriProvider Skill { get; }
    public FightUriProvider Fight { get; }
}