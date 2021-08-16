using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Translation.TranslatorClients.FunTranslations;

namespace TrueLayer.Services.Translation.Translators.Shakespeare
{
    public class ShakespeareTextTranslator : ITextTranslator
    {
        private readonly IFunTranslationsClient _funTranslationsClient;

        public ShakespeareTextTranslator(IFunTranslationsClient funTranslationsClient)
        {
            _funTranslationsClient = funTranslationsClient;
        }

        public Task<string> Translate(string textToTranslate, CancellationToken cancellationToken = default)
        {
            return _funTranslationsClient.Translate(FunTranslationsEndpoint.Shakespeare, textToTranslate, cancellationToken);
        }
    }
}
