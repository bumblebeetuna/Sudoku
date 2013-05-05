using System;
using System.Diagnostics.Contracts;
using Autofac;

namespace Sudoku.Application
{
    /// <summary>
    /// Command/Query access point for a part of the system
    /// </summary>
    [ContractClass(typeof(DomainContract))]
    public interface IDomain : IDisposable
    {
        /// <summary>
        /// The IoC container associated with this domain
        /// </summary>
        ILifetimeScope Container { get; }

        /// <summary>
        /// Executes a command against this domain
        /// </summary>
        /// <param name="command">The command specification to execute</param>
        void Execute(ICommand command);
        /// <summary>
        /// Executes a query against this domain
        /// </summary>
        /// <typeparam name="TResult">The return type of the query</typeparam>
        /// <param name="query">The query specification to execute</param>
        /// <returns>The result of the query operation</returns>
        TResult Execute<TResult>(IQuery<TResult> query);
    }

    [ContractClassFor(typeof(IDomain))]
    abstract class DomainContract : IDomain
    {
        #region IDomain Members

        public ILifetimeScope Container
        {
            get { Contract.Ensures(Contract.Result<ILifetimeScope>() != null); return default(ILifetimeScope); }
        }

        public void Execute(ICommand command)
        {
            Contract.Requires(command != null);
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            Contract.Requires(query != null);

            return default(TResult);
        }

        #endregion

        #region IDisposable Members

        public void Dispose() { }

        #endregion
    }
}
