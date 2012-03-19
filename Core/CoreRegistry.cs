using StructureMap.Configuration.DSL;
using Core.Configuration;
using Core.Infrastructure.Logging;
using Core.Services;

namespace Core
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {

            For<IShowService>()
                .Singleton()
                .Use<ShowService>();









            For<IAppConfigManager>()
                .Singleton()
                .Use<AppConfigManager>();

            For<IConnectionString>()
                .Singleton()
                .Use<ConnectionString>()
                .WithCtorArg("connectionStringKey").EqualTo("Shows");

            For<ILogWriter>()
                .HybridHttpOrThreadLocalScoped()
                .Use<DebuggerWriter>();
            SelectConstructor<DebuggerWriter>(() => new DebuggerWriter());
        }
    }
}
