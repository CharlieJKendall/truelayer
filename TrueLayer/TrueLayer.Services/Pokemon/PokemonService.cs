using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Clients;
using TrueLayer.Services.Pokemon.Models;
using TrueLayer.Services.Translation.Translators;

namespace TrueLayer.Services.Pokemon
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokeApiClient _pokeApiClient;
        private readonly ITextTranslatorFactory _translatorFactory;

        public PokemonService(IPokeApiClient pokeApiClient, ITextTranslatorFactory translatorFactory)
        {
            _pokeApiClient = pokeApiClient;
            _translatorFactory = translatorFactory;
        }

        public async Task<PokemonDto> GetPokemon(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var response = await _pokeApiClient.GetPokemon(name, cancellationToken);

            if (response is null)
            {
                return null;
            }

            // Clarification required here regarding what to do when english is not available - take another language? throw? return null?
            var description = response.FlavorTextEntries?.FirstOrDefault(e => e.Language?.Name?.ToLowerInvariant() == "en")?.FlavorText;

            // Would usually use AutoMapper for this
            var dto = new PokemonDto(response.Name, description, response.Habitat?.Name, response.IsLegendary);

            return dto;
        }

        public async Task<PokemonDto> GetTranslatedPokemon(string name, CancellationToken cancellationToken = default)
        {
            var pokemon = await GetPokemon(name, cancellationToken);

            if (pokemon is null)
            {
                return null;
            }

            pokemon = await TranslateDescription(pokemon, cancellationToken);

            return pokemon;
        }

        private async Task<PokemonDto> TranslateDescription(PokemonDto pokemon, CancellationToken cancellationToken)
        {
            // This is pretty inextensible, could create some more flexible infrastructure around this depending on future requirements
            var translatorType = pokemon.Habitat?.ToLowerInvariant() == "cave" || (pokemon.IsLegendary.HasValue && pokemon.IsLegendary.Value)
                ? TextTranslatorType.Yoda
                : TextTranslatorType.Shakespeare;

            var translator = _translatorFactory.Create(translatorType);
            var translated = await translator.Translate(pokemon.Description, cancellationToken);

            pokemon = pokemon with
            {
                Description = translated
            };

            return pokemon;
        }
    }
}
