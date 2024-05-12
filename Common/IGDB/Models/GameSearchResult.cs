using IGDB.Attributes;
using System.Text.Json.Serialization;

namespace IGDB.Models
{
    [IGDBUrl(Url = "https://api.igdb.com/v4/games")]
    public class GameSearchResult : IGDBModelBase
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cover")]
        public GameCover? Cover { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("url")]
        public required string Url { get; set; }

    }
}
