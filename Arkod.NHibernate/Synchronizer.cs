using System.Collections.Concurrent;

namespace Arkod.NHibernate
{
    /// <summary>
    /// Typed locking handler.
    /// </summary>
    /// <remarks><a href="https://stackoverflow.com/questions/781189/how-to-lock-on-an-integer-in-c"></a></remarks>
    /// <typeparam name="TKey">Locking type.</typeparam>
    public class Synchronizer<TKey>
    {
        /// <summary>
        /// Locks keeper
        /// </summary>
        private readonly ConcurrentDictionary<TKey, object> locks = new ConcurrentDictionary<TKey, object>();

        /// <summary>
        /// Obtains lock object based on given key.
        /// </summary>
        /// <param name="key">Given key.</param>
        public object this[TKey key] => locks.GetOrAdd(key, new object());
    }
}
