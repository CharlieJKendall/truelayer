using Autofac.Features.Indexed;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TrueLayer.Services.Translation.Translators;

namespace TrueLayer.Services.Tests.Translation
{
    [TestFixture]
    public class TextTranslationFactoryTests
    {
        private ITextTranslatorFactory _sut;
        private Mock<IIndex<TextTranslatorType, ITextTranslator>> _mockIndex;

        [SetUp]
        public void SetUp()
        {
            _mockIndex = new Mock<IIndex<TextTranslatorType, ITextTranslator>>();
            _sut = new TextTranslatorFactory(_mockIndex.Object, Mock.Of<ILogger<TextTranslatorFactory>>());
        }

        [Test]
        public void Create_ValidTextTranslatorTypeGiven_ReturnsCorrectTranslator()
        {
            // Arrange
            var mockTranslator = Mock.Of<ITextTranslator>();
            _mockIndex.Setup(m => m.TryGetValue(It.Is<TextTranslatorType>(t => t == TextTranslatorType.Shakespeare), out mockTranslator)).Returns(true);

            // Act
            var result = _sut.Create(TextTranslatorType.Shakespeare);

            // Assert
            Assert.That(result, Is.Not.Null);
        }
        
        [Test]
        public void Create_InvalidTextTranslatorTypeGiven_ReturnsNull()
        {
            // Arrange
            var mockTranslator = Mock.Of<ITextTranslator>();
            _mockIndex.Setup(m => m.TryGetValue(It.IsAny<TextTranslatorType>(), out mockTranslator)).Returns(false);

            // Act
            var result = _sut.Create(TextTranslatorType.Shakespeare);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
