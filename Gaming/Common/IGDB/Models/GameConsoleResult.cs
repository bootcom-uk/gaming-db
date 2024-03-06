using IGDB.Attributes;
using System.Text.Json.Serialization;

namespace IGDB.Models
{
    [IGDBUrl(Url = "https://api.igdb.com/v4/platforms")]
    public class GameConsoleResult
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("alternative_name")]
        public string? AlternativeName { get; set; }

        [JsonPropertyName("generation")]
        public int Generation { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("platform_family")]
        public int PlatformFamily { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("updated_at")]
        public int UpdatedAt { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("checksum")]
        public string? Checksum { get; set; }

        [JsonPropertyName("platform_logo")]
        public int? PlatformLogo { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        public PlatformLogo? PlatformLogoInformation { get; set; }

    }
}
