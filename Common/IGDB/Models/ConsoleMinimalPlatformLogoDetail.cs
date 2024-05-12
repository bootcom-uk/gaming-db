using IGDB.Attributes;
using System.Text.Json.Serialization;

namespace IGDB.Models
{
    [IGDBUrl(Url = "https://api.igdb.com/v4/platforms")]
    public class ConsoleMinimalPlatformLogoDetail : IGDBModelBase
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("platform_logo")]
        public int PlatformLogo { get; set; }

    }
}
