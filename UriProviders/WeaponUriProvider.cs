namespace RPGClient.UriProviders;

public class WeaponUriProvider
{
    public string Weapon { get; }

    public WeaponUriProvider()
    {
        Weapon = "API/Character/Weapon";
    }
}