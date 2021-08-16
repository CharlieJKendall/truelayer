﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Clients.Models.PokeApi;

namespace TrueLayer.Services.Clients
{
    public class PokeApiClient : IPokeApiClient
    {
        private const string BaseUrl = "https://pokeapi.co/api/v2";

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<PokeApiClient> _logger;

        public PokeApiClient(HttpClient httpClient, IMemoryCache cache, ILogger<PokeApiClient> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
            _logger = logger;
        }

        public async Task<PokeApiPokemonSpeciesResponse> GetPokemon(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var pokemon = await _cache.GetOrCreateAsync(name, async (cacheEntry) =>
            {
                try
                {
                    var url = $"{BaseUrl}/pokemon-species/{name}";
                    var response = await _httpClient.GetAsync(url, cancellationToken);

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsAsync<PokeApiPokemonSpeciesResponse>(cancellationToken);

                    return content;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Failed to get pokemon '{Name}'", name);
                    throw;
                }
            });

            return pokemon;
        }
    }
}
