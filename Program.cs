using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using RPGClient;
using RPGClient.Services;
using RPGClient.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IUriBuilderService>(new UriBuilderService("https://localhost:7110/"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(sp.GetService<IUriBuilderService>()!.BaseUri) });
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    config.SnackbarConfiguration.ShowCloseIcon = false;
});
builder.Services.AddScoped<ISessionStorageService, SessionStorageService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IWeaponService, WeaponService>();

await builder.Build().RunAsync();