using RPGClient.Services.Contracts;
using RPGClient.UriProviders;

namespace RPGClient.Services;

public class UriBuilderService : IUriBuilderService
{
    private AuthenticationUriProvider? _authenticationUriProvider;
    private CharacterUriProvider? _characterUriProvider;

    public string BaseUri { get; init; }

    public AuthenticationUriProvider Authentication => _authenticationUriProvider ??= new AuthenticationUriProvider();
    public CharacterUriProvider Character => _characterUriProvider ??= new CharacterUriProvider();

    public UriBuilderService(string baseUri)
    {
        BaseUri = baseUri;
    }
}