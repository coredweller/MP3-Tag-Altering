using StructureMap.Configuration.DSL;
using Repository;
using Core.Configuration;

namespace Data
{
    public class DataRegistry : Registry
    {

        public DataRegistry() {


            For<IShowRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ShowRepository>()
                .Ctor<IDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<ShowRepository>(() => new ShowRepository((IDatabaseFactory)null));

            For<ISetRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<SetRepository>()
                .Ctor<IDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<SetRepository>(() => new SetRepository((IDatabaseFactory)null));

            For<ISetSongRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<SetSongRepository>()
                .Ctor<IDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<SetSongRepository>(() => new SetSongRepository((IDatabaseFactory)null));







            For<Core.Infrastructure.IUnitOfWork>()
                        .HybridHttpOrThreadLocalScoped()
                        .Use<Repository.UnitOfWork>();
            SelectConstructor<Repository.UnitOfWork>( () => new Repository.UnitOfWork( (IDatabaseFactory)null ) );

            For<IDatabase>()
                .HybridHttpOrThreadLocalScoped()
                .Use<Database>()
                .Ctor<IConnectionString>("connectionString").IsTheDefault();
            SelectConstructor<Database>(() => new Database((IConnectionString)null));

            For<IDatabaseFactory>()
                .HybridHttpOrThreadLocalScoped()
                .Use<DatabaseFactory>();
        }
    }
}
