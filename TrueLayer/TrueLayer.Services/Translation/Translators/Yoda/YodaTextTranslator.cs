using System.Threading;
using System.Threading.Tasks;
using TrueLayer.Services.Translation.TranslatorClients.FunTranslations;

namespace TrueLayer.Services.Translation.Translators.Yoda
{
    public class YodaTextTranslator : ITextTranslator
    {
        private readonly IFunTranslationsClient _funTranslationsClient;

        public YodaTextTranslator(IFunTranslationsClient funTranslationsClient)
        {
            _funTranslationsClient = funTranslationsClient;
        }

        public Task<string> Translate(string textToTranslate, CancellationToken cancellationToken = default)
        {
            return _funTranslationsClient.Translate(FunTranslationsEndpoint.Yoda, textToTranslate, cancellationToken);
        }
    }
}
