using RPGClient.Services.Contracts;

namespace RPGClient.Extensions.AddServices
{
    public static class AddHttpClientExtension
    {
        public static IServiceCollection AddHttpClientService(this IServiceCollection services)
        {
            return services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(sp.GetService<IUriBuilderService>()!.BaseUri) });
        }
    }
}