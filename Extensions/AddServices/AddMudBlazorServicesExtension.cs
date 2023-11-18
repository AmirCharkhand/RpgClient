using MudBlazor;
using MudBlazor.Services;

namespace RPGClient.Extensions.AddServices
{
    public static class AddMudBlazorServicesExtension
    {
        public static IServiceCollection AddMudBlazorServices(this IServiceCollection services)
        {
            return services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
                config.SnackbarConfiguration.ShowCloseIcon = false;
            });
        }
    }
}