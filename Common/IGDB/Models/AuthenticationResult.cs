using System.Text.Json.Serialization;

namespace IGDB.Models
{
    internal class AuthenticationResult
    {

        [JsonPropertyName("access_token")]
        public required string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public required string TokenType { get; set; }

    }
}
