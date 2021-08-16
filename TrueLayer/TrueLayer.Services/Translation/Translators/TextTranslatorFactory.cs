using Autofac.Features.Indexed;

namespace TrueLayer.Services.Translation.Translators
{
    public class TextTranslatorFactory : ITextTranslatorFactory
    {
        IIndex<TextTranslatorType, ITextTranslator> _textTranslators;

        public TextTranslatorFactory(IIndex<TextTranslatorType, ITextTranslator> textTranslators)
        {
            _textTranslators = textTranslators;
        }

        public ITextTranslator Create(TextTranslatorType textTranslator)
        {
            return _textTranslators.TryGetValue(textTranslator, out var value) ? value : null;
        }
    }
}
