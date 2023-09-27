using RPGClient.Services.Contracts;
using RPGClient.UriProviders;

namespace RPGClient.Services;

public class UriBuilderService : IUriBuilderService
{
    private AuthenticationUriProvider? _authenticationUriProvider;
    private CharacterUriProvider? _characterUriProvider;
    private WeaponUriProvider? _weaponUriProvider;
    private SkillUriProvider? _skillUriProvider;

    public string BaseUri { get; init; }

    public AuthenticationUriProvider Authentication => _authenticationUriProvider ??= new AuthenticationUriProvider();
    public CharacterUriProvider Character => _characterUriProvider ??= new CharacterUriProvider();
    public WeaponUriProvider Weapon => _weaponUriProvider ??= new WeaponUriProvider();
    public SkillUriProvider Skill => _skillUriProvider ??= new SkillUriProvider();

    public UriBuilderService(string baseUri)
    {
        BaseUri = baseUri;
    }
}