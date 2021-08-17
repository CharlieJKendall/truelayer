using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Clients;
using TrueLayer.Services.Clients.Models.PokeApi;
using TrueLayer.Services.Pokemon;
using TrueLayer.Services.Translation.Translators;

namespace TrueLayer.Services.Tests.Pokemon
{
    [TestFixture]
    public class PokemonServiceTests
    {
        private IPokemonService _sut;
        private Mock<IPokeApiClient> _mockClient;
        private Mock<ITextTranslatorFactory> _mockTextTranslationFactory;

        [SetUp]
        public void SetUp()
        {
            _mockClient = new Mock<IPokeApiClient>();
            _mockTextTranslationFactory = new Mock<ITextTranslatorFactory>();
            _sut = new PokemonService(_mockClient.Object, _mockTextTranslationFactory.Object);
        }

        [TestCase("")]
        [TestCase(null)]
        public async Task GetPokemon_InvalidName_ReturnsNull(string name)
        {
            // Act
            var result = await _sut.GetPokemon(name);

            // Assert
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public async Task GetPokemon_ClientReturnsNull_ReturnsNull()
        {
            // Arrange
            var name = "name";
            _mockClient.Setup(m => m.GetPokemon(It.Is<string>(x => x == name), It.IsAny<CancellationToken>())).ReturnsAsync((PokeApiPokemonSpeciesResponse)null);

            // Act
            var result = await _sut.GetPokemon(name);

            // Assert
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public async Task GetPokemon_PokemonWithMultipleFlavorTextEntries_ReturnsEnglishEntry()
        {
            // Arrange
            var name = "name";
            var flavorText = "english";
            var response = new PokeApiPokemonSpeciesResponse()
            {
                FlavorTextEntries = new FlavorTextEntry[]
                {
                    new FlavorTextEntry() { Language = new Language() { Name = "fr" }, FlavorText = "french" },
                    new FlavorTextEntry() { Language = new Language() { Name = "en" }, FlavorText = flavorText },
                }
            };

            _mockClient.Setup(m => m.GetPokemon(It.Is<string>(x => x == name), It.IsAny<CancellationToken>())).ReturnsAsync(response);

            // Act
            var result = await _sut.GetPokemon(name);

            // Assert
            Assert.That(result.Description, Is.EqualTo(flavorText));
        }
        
        [Test]
        public async Task GetPokemon_PokemonWithoutEnglishFlavourTextEntry_ReturnsNullDescription()
        {
            // Arrange
            var name = "name";
            var response = new PokeApiPokemonSpeciesResponse()
            {
                FlavorTextEntries = new FlavorTextEntry[]
                {
                    new FlavorTextEntry() { Language = new Language() { Name = "fr" }, FlavorText = "french" }
                }
            };

            _mockClient.Setup(m => m.GetPokemon(It.Is<string>(x => x == name), It.IsAny<CancellationToken>())).ReturnsAsync(response);

            // Act
            var result = await _sut.GetPokemon(name);

            // Assert
            Assert.That(result.Description, Is.Null);
        }
        
        [Test]
        public async Task GetTranslatedPokemon_PokemonWithoutEnglishFlavourTextEntry_ReturnsNullDescription()
        {
            // Arrange
            var name = "name";
            var response = new PokeApiPokemonSpeciesResponse()
            {
                FlavorTextEntries = new FlavorTextEntry[]
                {
                    new FlavorTextEntry() { Language = new Language() { Name = "fr" }, FlavorText = "french" }
                }
            };

            _mockClient.Setup(m => m.GetPokemon(It.Is<string>(x => x == name), It.IsAny<CancellationToken>())).ReturnsAsync(response);
            _mockTextTranslationFactory
                .Setup(m => m.Create(It.IsAny<TextTranslatorType>()))
                .Returns(Mock.Of<ITextTranslator>(t => t.Translate(It.Is<string>(x => x == null), It.IsAny<CancellationToken>()) == Task.FromResult((string)null)));

            // Act
            var result = await _sut.GetTranslatedPokemon(name);

            // Assert
            Assert.That(result.Description, Is.Null);
            _mockTextTranslationFactory.Verify(m => m.Create(It.IsAny<TextTranslatorType>()), Times.Once);
        }
    }
}
