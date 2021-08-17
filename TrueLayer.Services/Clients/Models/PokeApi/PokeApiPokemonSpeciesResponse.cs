using System.Text.Json.Serialization;

namespace TrueLayer.Services.Clients.Models.PokeApi
{
    public class PokeApiPokemonSpeciesResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("flavor_text_entries")]
        public FlavorTextEntry[] FlavorTextEntries { get; set; }
        
        [JsonPropertyName("habitat")]
        public Habitat Habitat { get; set; }
        
        [JsonPropertyName("is_legendary")]
        public bool? IsLegendary { get; set; }
    }
}
