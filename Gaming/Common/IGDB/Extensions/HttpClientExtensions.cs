namespace IGDB.Extensions
{
    public static class HttpClientExtensions
    {

        public async static Task<HttpResponseMessage> MakeRequest(this HttpClient httpClient, HttpRequestMessage message)
        {
            return await httpClient.SendAsync(message);
        }

    }
}
