using System.Text.Json.Serialization;

namespace TrueLayer.Services.Clients.Models.PokeApi
{
    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }     
    }
}
