using System.Diagnostics.Contracts;
using Autofac;

namespace Sudoku.Application
{
    /// <summary>
    /// Default factory for creating instances of <see cref="Bookkeepr.Application.IDomain"/> instances
    /// </summary>
    public class DomainFactory : IDomainFactory
    {
        private readonly ILifetimeScope container;

        /// <summary>
        /// Contstructs a new instance of <see cref="Bookkeepr.Application.DomainFactory"/> 
        /// </summary>
        /// <param name="container">An IoC container to use for resolving types</param>
        public DomainFactory(ILifetimeScope container)
        {
            Contract.Requires(container != null);

            this.container = container;
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(container != null);
        }

        #region IDomainFactory Members

        /// <summary>
        /// Creates an instance of <see cref="Bookkeepr.Application.IDomain"/>
        /// </summary>
        /// <param name="dataSource">The datasource that the <see cref="Bookkeepr.Application.IDomain"/> instance will be bound to</param>
        /// <returns>A new instance of <see cref="Bookkeepr.Application.IDomain"/></returns>
        public IDomain Create(IDataSource dataSource)
        {
            var result = container.Resolve<Domain>(new PositionalParameter(0, container), new PositionalParameter(1, dataSource));

            Contract.Assume(result != null);

            return result;
        }

        #endregion
    }
}
