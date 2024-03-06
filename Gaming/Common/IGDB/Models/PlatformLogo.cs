using System.Text.Json.Serialization;

namespace IGDB.Models
{
    public class PlatformLogo : IGDBModelBase
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("alpha_channel")]
        public bool AlphaChannel { get; set; }

        [JsonPropertyName("animated")]
        public bool Animated { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("image_id")]
        public string ImageId { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("checksum")]
        public string Checksum { get; set; }

    }
}
