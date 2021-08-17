using System.Text.Json.Serialization;

namespace TrueLayer.Services.Translation.TranslatorClients.FunTranslations.Models
{
    public class FunTranslationsApiResponse
    {
        [JsonPropertyName("success")]
        public Success Success { get; set; }

        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }
    }
}
