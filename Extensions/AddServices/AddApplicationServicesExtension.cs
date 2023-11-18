using RPGClient.Services.Contracts;
using RPGClient.Services;

namespace RPGClient.Extensions.AddServices
{
    public static class AddApplicationServicesExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddUriBuilderService()
                .AddHttpClientService()
                .AddMudBlazorServices()
                .AddScoped<ISessionStorageService, SessionStorageService>()
                .AddScoped<ICharacterService, CharacterService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddTransient<IWeaponService, WeaponService>()
                .AddTransient<ISkillService, SkillService>()
                .AddTransient<IFightService, FightService>();
        }
    }
}
