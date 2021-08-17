namespace TrueLayer.Services.Translation.Translators
{
    public interface ITextTranslatorFactory
    {
        ITextTranslator Create(TextTranslatorType textTranslator);
    }
}