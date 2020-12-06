using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using NHibernate;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace Arkod.NHibernate.Tests
{
    /// <summary>
    /// Provide a NHibernate session scope for in memory database testing
    /// </summary>
    [TestClass]
    public abstract class InMemoryDatabaseTests
    {
        public abstract Assembly Assembly { get;  }

        /// <summary>
        /// Session scoped to each test. Do not dispose.
        /// </summary>
        protected ISession Session { get; private set; }

        /// <summary>
        /// Transaction of the test
        /// </summary>
        private ITransaction Transaction { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var sessionManager = new InMemorySessionManager();
            Session = sessionManager.OpenSession(Assembly);
            Transaction = Session.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
            Session.Dispose();
            Session = null;
        }
    }
}