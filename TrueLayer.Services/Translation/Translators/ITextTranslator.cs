using System.Threading;
using System.Threading.Tasks;

namespace TrueLayer.Services.Translation.Translators
{
    public interface ITextTranslator
    {
        public Task<string> Translate(string textToTranslate, CancellationToken cancellationToken = default);
    }
}
