using NUnit.Framework;
using PublicApi.Client;
using System.Threading.Tasks;

namespace TrueLayer.Api.Tests.Controllers
{
    [TestFixture]
    public class PokemonControllerTests
    {
        private IPokemonClient _pokemonClient;

        [SetUp]
        public void SetUp()
        {
            var httpClient = ApiWebApplicationFactory.CreateHttpClient();
            _pokemonClient = new PokemonClient("http://localhost", httpClient);
        }

        [Test]
        public async Task GetPokemon_ExistingPokemon_ReturnsCorrectDataAndResponse()
        {
            // Arrange
            var name = "mewtwo";

            // Act
            var result = await _pokemonClient.GetAsync(name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Description, Is.EqualTo("It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments."));
                Assert.That(result.Habitat, Is.EqualTo("rare"));
                Assert.That(result.IsLegendary, Is.True);
                Assert.That(result.Name, Is.EqualTo(name));
            });
        }
        
        [Test]
        public void GetPokemon_NonExistingPokemon_ThrowsApiException()
        {
            // Arrange
            var name = "non-existant";

            // Act
            Task del() => _pokemonClient.GetAsync(name);

            // Assert
            Assert.ThrowsAsync<ApiException>(del);
        }
    }
}
