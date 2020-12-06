using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace Arkod.NHibernate
{
    /// <summary>
    /// Opens <see cref="ISession"/> on a Sqlite in memory databases
    /// </summary>
    public class InMemorySessionManager : ISessionManager
    {
        private static readonly List<DbConnection> ConnectionPersistence = new List<DbConnection>();
        private static readonly Dictionary<Assembly, ISessionFactory> SessionFactoryCache = new Dictionary<Assembly, ISessionFactory>();

        private static readonly Synchronizer<Assembly> assemblySynchronizer = new Synchronizer<Assembly>();

        /// <summary>
        /// Build a configuration & session factory by a given assembly.
        /// </summary>
        private ISessionFactory BuildSessionFactory(Assembly assembly)
        {
            var configuration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString($"FullUri=file:{assembly.FullName}.db?mode=memory&cache=shared").Driver<SQLite20Driver>())
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssembly(assembly).Conventions.Add(AutoImport.Never());
                    m.HbmMappings.AddFromAssembly(assembly);
                })
                .AutoQuoteDDLKeywords()
                .BuildConfiguration();
            var sessionFactory = configuration.BuildSessionFactory();
            // open session
            var session = sessionFactory.OpenSession();
            // schema creation
            new SchemaExport(configuration).Execute(true, true, false, session.Connection, null);
            // store the connection for keeping the database in memory
            ConnectionPersistence.Add(session.Connection);
            return sessionFactory;
        }

        /// <summary>
        /// Récupère la session de la base mémoire en cache
        /// </summary>
        public ISession OpenSession(Assembly assembly)
        {
            if (!SessionFactoryCache.ContainsKey(assembly))
            {
                lock (assemblySynchronizer[assembly])
                {
                    if (!SessionFactoryCache.ContainsKey(assembly))
                    {
                        SessionFactoryCache.Add(assembly, BuildSessionFactory(assembly));
                    }
                }
            }
            return SessionFactoryCache[assembly].OpenSession();
        }
    }
}
