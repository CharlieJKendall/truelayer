using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Pokemon;
using TrueLayer.Services.Pokemon.Models;

namespace TrueLayer.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<PokemonDto>> Get([FromRoute] string name, CancellationToken cancellationToken)
        {
            return await HandleGetByName(name, _pokemonService.GetPokemon, cancellationToken);
        }

        [HttpGet("translated/{name}")]
        public async Task<ActionResult<PokemonDto>> GetTranslated([FromRoute] string name, CancellationToken cancellationToken)
        {
            return await HandleGetByName(name, _pokemonService.GetTranslatedPokemon, cancellationToken);
        }

        private async Task<ActionResult<PokemonDto>> HandleGetByName(string name, Func<string, CancellationToken, Task<PokemonDto>> getByName, CancellationToken cancellation)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            var pokemon = await getByName(name, cancellation);

            if (pokemon is null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }
    }
}
