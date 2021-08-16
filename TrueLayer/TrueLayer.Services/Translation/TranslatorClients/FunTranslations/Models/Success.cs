using System.Text.Json.Serialization;

namespace TrueLayer.Services.Translation.TranslatorClients.FunTranslations.Models
{
    public class Success
    {
        [JsonPropertyName("total")]
        public int? Total { get; set; }
    }
}
