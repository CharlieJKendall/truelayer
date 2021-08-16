using Autofac;
using TrueLayer.Services.Clients;
using TrueLayer.Services.Pokemon;
using TrueLayer.Services.Translation.TranslatorClients.FunTranslations;
using TrueLayer.Services.Translation.Translators;
using TrueLayer.Services.Translation.Translators.Shakespeare;
using TrueLayer.Services.Translation.Translators.Yoda;

namespace TrueLayer.Services
{
    public class ServicesDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TextTranslatorFactory>().As<ITextTranslatorFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ShakespeareTextTranslator>().Keyed<ITextTranslator>(TextTranslatorType.Shakespeare);
            builder.RegisterType<YodaTextTranslator>().Keyed<ITextTranslator>(TextTranslatorType.Yoda);
            builder.RegisterType<FunTranslationsClient>().As<IFunTranslationsClient>().InstancePerLifetimeScope();
            builder.RegisterType<PokemonService>().As<IPokemonService>().InstancePerLifetimeScope();
            builder.RegisterType<PokeApiClient>().As<IPokeApiClient>().InstancePerLifetimeScope();
        }
    }
}
