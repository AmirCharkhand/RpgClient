using RPGClient.Services.Contracts;
using RPGClient.Services;

namespace RPGClient.Extensions.AddServices
{
    public static class AddUriBuilderExtension
    {
        public static IServiceCollection AddUriBuilderService(this IServiceCollection services)
        {
            return services.AddSingleton<IUriBuilderService>(new UriBuilderService("https://localhost:7110/"));
        }
    }
}
