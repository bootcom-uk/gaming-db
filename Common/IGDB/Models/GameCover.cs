using System.Text.Json.Serialization;

namespace IGDB.Models
{
    public class GameCover : IGDBModelBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public required string Url { get; set; }
    }
}
