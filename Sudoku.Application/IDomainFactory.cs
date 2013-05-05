using System.Diagnostics.Contracts;

namespace Sudoku.Application
{
    /// <summary>
    /// Factory for creating instances of <see cref="IDomain"/>
    /// </summary>
    [ContractClass(typeof(DomainFactoryContract))]
    public interface IDomainFactory
    {
        /// <summary>
        /// Creates and instance of an <see cref="IDomain"/> linked to the specified <see cref="IDataSource"/>.
        /// </summary>
        /// <param name="dataSource">The data source to link the domain to.</param>
        /// <returns>A new domain linked to the specified data source</returns>
        IDomain Create(IDataSource dataSource);
    }

    [ContractClassFor(typeof(IDomainFactory))]
    abstract class DomainFactoryContract : IDomainFactory
    {
        #region IDomainFactory Members

        public IDomain Create(IDataSource dataSource)
        {
            Contract.Ensures(Contract.Result<IDomain>() != null);

            return default(IDomain);
        }

        #endregion
    }
}
