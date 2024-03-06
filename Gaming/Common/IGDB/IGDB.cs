using IGDB.Models;

namespace IGDB
{
    public class IGDB
    {

        public IGDBWebClient? WebClient { get; }

        public IGDB(string clientId, string clientSecret)
        {
            WebClient = new IGDBWebClient(clientId, clientSecret);
        }


        public async Task<string?> GetPlatformCover(int igdbConsoleId)
        {

            var consoleSearch = $"where id={igdbConsoleId};\r\nfields platform_logo;";
            var consoleSearchListResponse = await WebClient!.GetIGDBRecords<ConsoleMinimalPlatformLogoDetail>(searchCriteria: consoleSearch);
            if (consoleSearchListResponse is null || consoleSearchListResponse.Count() == 0) return null;
            var consoleSearchItem = consoleSearchListResponse.First();

            var platformImageSearch = $"where id={consoleSearchItem.PlatformLogo};\r\nfields url;";
            var platformImageListResponse = await WebClient!.GetIGDBRecords<PlatformMinimalLogoDetail>(searchCriteria: platformImageSearch);
            if (platformImageListResponse is null || platformImageListResponse.Count() == 0) return null;
            return platformImageListResponse.First().Url;
        }

        public async Task<IEnumerable<GameSearchResult>?> SearchGame(string searchTerm, int? igdbConsoleId)
        {
            var searchBody = $"search \\\\\"{searchTerm}\\\\\";{Environment.NewLine}";
            if (igdbConsoleId != null)
            {
                searchBody += $"where platforms = ({igdbConsoleId});{Environment.NewLine}";
            }
            searchBody += "fields id, cover.url,name,url,summary;" + Environment.NewLine;
            searchBody += "limit 500;";
            return await WebClient!.GetIGDBRecords<GameSearchResult>(searchCriteria: searchBody);
        }

        public async Task<GameSearchResult?> GetGameDetail(int igdbId)
        {
            var searchBody = $"where id = ({igdbId});{Environment.NewLine}";            
            searchBody += "fields id, cover.url,name,url,summary;" + Environment.NewLine;
            var results = await WebClient!.GetIGDBRecords<GameSearchResult>(searchCriteria: searchBody);
            if(results is null) return null;
            return results!.FirstOrDefault();
        }

    }
}
