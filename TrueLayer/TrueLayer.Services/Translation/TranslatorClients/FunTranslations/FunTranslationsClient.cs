using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Translation.TranslatorClients.FunTranslations.Models;

namespace TrueLayer.Services.Translation.TranslatorClients.FunTranslations
{
    public class FunTranslationsClient : IFunTranslationsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FunTranslationsClient> _logger;
        private readonly IMemoryCache _memoryCache;

        private const string BaseUrl = "https://api.funtranslations.com";

        public FunTranslationsClient(HttpClient httpClient, ILogger<FunTranslationsClient> logger, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<string> Translate(FunTranslationsEndpoint endpoint, string textToTranslate, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(textToTranslate))
            {
                return textToTranslate;
            }

            var translated = await _memoryCache.GetOrCreateAsync(
                textToTranslate,
                (cacheEntry) => TranslateText(cacheEntry, endpoint, textToTranslate, cancellationToken));

            return translated;
        }

        private async Task<string> TranslateText(ICacheEntry cacheEntry, FunTranslationsEndpoint endpoint, string textToTranslate, CancellationToken cancellationToken)
        {
            // If there was an OpenAPI/Swagger doc, generate NSwag clients instead of writing the logic manually
            try
            {
                var endpointExtension = GetEndpoint(endpoint);
                var url = $"{BaseUrl}/translate/{endpointExtension}";

                var bodyJson = JsonSerializer.Serialize(new { text = textToTranslate });
                var body = new StringContent(bodyJson, Encoding.UTF8, "application/json");
                using var response = await _httpClient.PostAsync(url, body, cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsAsync<FunTranslationsApiResponse>(cancellationToken);
                var result = responseContent.Contents?.Translated;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to translate text using FunTranslations translator");
                cacheEntry.AbsoluteExpiration = DateTime.Now;
            }

            return textToTranslate;
        }

        private static string GetEndpoint(FunTranslationsEndpoint endpoint) => endpoint switch
        {
            FunTranslationsEndpoint.Yoda => "yoda.json",
            FunTranslationsEndpoint.Shakespeare => "shakespeare.json",
            _ => throw new InvalidOperationException($"No endpoint has been defined for '{endpoint}'")
        };
    }
}
