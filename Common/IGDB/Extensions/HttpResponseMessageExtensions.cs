using IGDB.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace IGDB.Extensions
{
    public static class HttpResponseMessageExtensions
    {

        public static bool IsAuthenticated(this HttpResponseMessage responseMessage) {
            if (responseMessage == null) throw new ArgumentNullException(nameof(responseMessage));
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized) return false;
            return true;
        }
        
        public async static Task<LocalHttpResponse?> AuthenticateAndRetryIfRequired(this HttpResponseMessage responseMessage, HttpClient httpClient, string clientId, string clientSecret)
        {
            var result = new LocalHttpResponse();

            if (responseMessage == null) throw new ArgumentNullException(nameof(responseMessage));

            if (responseMessage.IsAuthenticated())
            {
                result.WebResponse = responseMessage;
                return result;
            }

            var requestMessage = await responseMessage.RequestMessage!.Clone();

            var authenticationMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials")
            };
            authenticationMessage.Headers.Add("User-Agent", "BootCom");
            authenticationMessage.Method = HttpMethod.Post;
            var authenticationResponseMessage = await httpClient.SendAsync(authenticationMessage);

            if (!authenticationResponseMessage.IsSuccessStatusCode) return null;

            var authResult = await JsonSerializer.DeserializeAsync<AuthenticationResult>(await authenticationResponseMessage.Content.ReadAsStreamAsync());
            if (authResult is null) return null;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);

            result.BearerToken = authResult.AccessToken;
            result.WebResponse = await httpClient.MakeRequest(requestMessage);

            return result;
        }

    }
}
