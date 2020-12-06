using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arkod.NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using NFluent;
using System.Reflection;
using Models.Testing;
using System.Linq;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace Arkod.NHibernate.Tests
{
    [TestClass]
    public class InMemorySessionManagerTests
    {
        private static Assembly TestingAssembly = Assembly.Load("Models.Testing");

        /// <summary>
        /// Ensure that test session can be resolved
        /// </summary>
        [TestMethod]
        public void SessionResolve()
        {
            // resolve session
            var sessionManager = new InMemorySessionManager();
            using (var session = sessionManager.OpenSession(TestingAssembly))
            {
                Check.That(session).IsNotNull();
            }
        }

        /// <summary>
        /// Ensure that session is using the same database
        /// </summary>
        [TestMethod]
        [DoNotParallelize]
        public void SessionDatabaseScope()
        {
            // resolve session
            var sessionManager = new InMemorySessionManager();
            using (var session = sessionManager.OpenSession(TestingAssembly))
            {
                var posts = session.Query<Post>().ToList();
                Check.That(posts).IsEmpty();

                // insert testing
                var post = new Post { Title = "Un titre", Author = "Moi", Content = "Blablabla" };
                session.Save(post);
                // session is flushed by Save because post have an auto increment Id that must be knowned by NHibernate
            }

            using (var session = sessionManager.OpenSession(TestingAssembly))
            {
                var post = session.Query<Post>().SingleOrDefault();
                Check.That(post).IsNotNull();
                session.Delete(post);
                session.Flush();
            }

            using (var session = sessionManager.OpenSession(TestingAssembly))
            {
                var posts = session.Query<Post>().ToList();
                Check.That(posts).IsEmpty();
            }
        }

        [TestMethod]
        public void SessionTransactionScope()
        {
            var sessionManager = new InMemorySessionManager();
            using (var session = sessionManager.OpenSession(TestingAssembly))
            {
                using (var transaction = session.BeginTransaction())
                {
                    var post = new Post { Title = "Un titre", Author = "Moi", Content = "Blablabla" };
                    session.Save(post);
                    transaction.Rollback();
                }
            }
        }
    }
}