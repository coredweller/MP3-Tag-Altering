using StructureMap.Configuration.DSL;
using Repository;
using Core.Configuration;

namespace Data
{
    public class DataRegistry : Registry
    {

        public DataRegistry() {

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
