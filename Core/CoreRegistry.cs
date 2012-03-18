using StructureMap.Configuration.DSL;
using Core.Configuration;
using Core.Infrastructure.Logging;

namespace Core
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
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
