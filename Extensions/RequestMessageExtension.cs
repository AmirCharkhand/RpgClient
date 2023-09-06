using System.Net.Http.Headers;
using RPGClient.Services.Contracts;

namespace RPGClient.Extensions;

public static class RequestMessageExtension
{
    public static async Task<HttpRequestMessage> NewGetRequestMessageWithBearer(this HttpRequestMessage requestMessage, string uri, ISessionStorageService sessionStorageService)
    {
        var bearer = await sessionStorageService.GetItem<string>("UserToken");
        requestMessage.Method=HttpMethod.Get;
        requestMessage.RequestUri = new Uri(uri, UriKind.Relative);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", bearer);
        return requestMessage;
    }
}