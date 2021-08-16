using System.Threading;
using System.Threading.Tasks;

namespace TrueLayer.Services.Translation.TranslatorClients.FunTranslations
{
    public interface IFunTranslationsClient
    {
        Task<string> Translate(FunTranslationsEndpoint endpoint, string textToTranslate, CancellationToken cancellationToken = default);
    }
}