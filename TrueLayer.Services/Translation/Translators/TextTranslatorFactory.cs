using Autofac.Features.Indexed;
using Microsoft.Extensions.Logging;

namespace TrueLayer.Services.Translation.Translators
{
    public class TextTranslatorFactory : ITextTranslatorFactory
    {
        IIndex<TextTranslatorType, ITextTranslator> _textTranslators;
        ILogger<TextTranslatorFactory> _logger;

        public TextTranslatorFactory(IIndex<TextTranslatorType, ITextTranslator> textTranslators, ILogger<TextTranslatorFactory> logger)
        {
            _textTranslators = textTranslators;
            _logger = logger;
        }

        public ITextTranslator Create(TextTranslatorType textTranslator)
        {
            if (_textTranslators.TryGetValue(textTranslator, out var value))
            {
                return value;
            }
            else
            {
                _logger.LogWarning("No text translator is registered with the dependency container for '{TextTranslatorType}'", textTranslator);
                return null;
            }
        }
    }
}
