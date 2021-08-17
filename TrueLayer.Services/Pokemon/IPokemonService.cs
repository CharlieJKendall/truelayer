using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Pokemon.Models;

namespace TrueLayer.Services.Pokemon
{
    public interface IPokemonService
    {
        Task<PokemonDto> GetPokemon(string name, CancellationToken cancellationToken = default);
        Task<PokemonDto> GetTranslatedPokemon(string name, CancellationToken cancellationToken = default);
    }
}