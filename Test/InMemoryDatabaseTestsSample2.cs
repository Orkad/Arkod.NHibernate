using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using System.Reflection;
using Models.Testing;
using System.Linq;

namespace Arkod.NHibernate.Tests
{
    [TestClass]
    public class InMemoryDatabaseTestsSample2 : InMemoryDatabaseTests
    {
        public override Assembly Assembly => Assembly.Load("Models.Testing");

        [TestMethod]
        public void DoSomething1()
        {
            var post = new Post { Title = "InMemoryDatabaseTestsSample2", Author = "Moi", Content = "DoSomething1" };
            Session.Save(post);
            Check.That(Session.Query<Post>().Count()).IsEqualTo(1);
            Check.That(Session.Query<Post>().Single().Title).IsEqualTo("InMemoryDatabaseTestsSample2");
            Check.That(Session.Query<Post>().Single().Content).IsEqualTo("DoSomething1");
        }

        [TestMethod]
        public void DoSomething2()
        {
            var post = new Post { Title = "InMemoryDatabaseTestsSample2", Author = "Moi", Content = "DoSomething2" };
            Session.Save(post);
            Check.That(Session.Query<Post>().Count()).IsEqualTo(1);
            Check.That(Session.Query<Post>().Single().Title).IsEqualTo("InMemoryDatabaseTestsSample2");
            Check.That(Session.Query<Post>().Single().Content).IsEqualTo("DoSomething2");
        }

        [TestMethod]
        public void DoSomething3()
        {
            var post = new Post { Title = "InMemoryDatabaseTestsSample2", Author = "Moi", Content = "DoSomething3" };
            Session.Save(post);
            Check.That(Session.Query<Post>().Count()).IsEqualTo(1);
            Check.That(Session.Query<Post>().Single().Title).IsEqualTo("InMemoryDatabaseTestsSample2");
            Check.That(Session.Query<Post>().Single().Content).IsEqualTo("DoSomething3");
        }
    }
}