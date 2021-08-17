using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Clients.Models.PokeApi;

namespace TrueLayer.Services.Clients
{
    public interface IPokeApiClient
    {
        Task<PokeApiPokemonSpeciesResponse> GetPokemon(string name, CancellationToken cancellationToken = default);
    }
}