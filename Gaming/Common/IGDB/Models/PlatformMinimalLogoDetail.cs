using IGDB.Attributes;
using System.Text.Json.Serialization;

namespace IGDB.Models
{
    [IGDBUrl(Url = "https://api.igdb.com/v4/platform_logos")]
    public class PlatformMinimalLogoDetail : IGDBModelBase
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public required string Url { get; set; }

    }
}
