using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging.Messages;
using IGDB.Configuration;
using IGDB.Extensions;
using IGDB.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace IGDB
{
    public sealed partial class IGDBWebClient(string clientId, string clientSecret) : ObservableObject
    {

        [ObservableProperty]
        string? bearerToken;

        internal async Task<HttpResponseMessage?> GetMessage<IGDBRecordType>(string fields = "", string searchCriteria = "") where IGDBRecordType : IGDBModelBase
        {
            var client = new HttpClient();

            var recordType = typeof(IGDBRecordType);
            var url = recordType.GetIGDBUrl();

            if (url == null) { return null; }

            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Content = new StringContent(searchCriteria),
                Method = HttpMethod.Post
            };
            httpRequestMessage.Headers.Add("Client-ID", clientId);
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", BearerToken);

            var responseMessage = await client.SendAsync(httpRequestMessage);

            var response = await responseMessage.AuthenticateAndRetryIfRequired(client, clientId, clientSecret);

            if (response is null) { return null; }

            responseMessage = response!.WebResponse;

            if (string.IsNullOrEmpty(BearerToken))
            {
                BearerToken = response.BearerToken;
            }

            if (responseMessage is null) { return null; }

            return responseMessage;
        }

        public async Task<IEnumerable<IGDBRecordType>?> GetIGDBRecords<IGDBRecordType>(string fields = "", string searchCriteria = "") where IGDBRecordType : IGDBModelBase
        {
            var responseMessage = await GetMessage<IGDBRecordType>(fields, searchCriteria);

            if(responseMessage is null) { return null;}

            return await JsonSerializer.DeserializeAsync<IEnumerable<IGDBRecordType>>(await responseMessage.Content.ReadAsStreamAsync());
        }

        public async Task<IGDBRecordType?> GetIGDBRecord<IGDBRecordType>(string fields = "", string searchCriteria = "") where IGDBRecordType : IGDBModelBase
        {
            var responseMessage = await GetMessage<IGDBRecordType>(fields, searchCriteria);

            if (responseMessage is null) { return null; }

            return await JsonSerializer.DeserializeAsync<IGDBRecordType>(await responseMessage.Content.ReadAsStreamAsync());
        }

    }
}
