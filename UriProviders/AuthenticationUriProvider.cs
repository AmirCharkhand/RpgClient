namespace RPGClient.UriProviders;

public class AuthenticationUriProvider
{
    public string Authentication { get; }
    public string GetUriForLogin
    {
        get => Authentication + "/login";
    }

    public AuthenticationUriProvider()
    {
        Authentication = "API/Auth";
    }
}