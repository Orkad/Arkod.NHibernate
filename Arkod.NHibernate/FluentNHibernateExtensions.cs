using FluentNHibernate.Cfg;
using NHibernate.Cfg;

namespace Arkod.NHibernate
{
    /// <summary>
    /// Défini des méthodes d'extensions pour la configuration fluent de nhibernate
    /// </summary>
    public static class FluentNHibernateExtensions
    {
        /// <summary>
        /// Rajoute automatiquement des quotes sur les nom de table / colonne qui correspondraient a des mots clefs
        /// </summary>
        public static FluentConfiguration AutoQuoteDDLKeywords(this FluentConfiguration fluentConfiguration)
            => fluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", Hbm2DDLKeyWords.AutoQuote.ToString()));
    }
}
