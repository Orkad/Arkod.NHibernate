using NHibernate;
using System;
using System.Reflection;

namespace Arkod.NHibernate
{
    /// <summary>
    /// Opens <see cref="ISession"/> by a given <see cref="Assembly"/>.
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// Returns openned & disposable <see cref="ISession"/> by the given <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">Given assembly</param>
        ISession OpenSession(Assembly assembly);
    }
}
