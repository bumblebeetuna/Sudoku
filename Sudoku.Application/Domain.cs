using System;
using System.Diagnostics.Contracts;
using Autofac;
using Sudoku.Infrastructure;

namespace Sudoku.Application
{
    /// <summary>
    /// Default implementation of <see cref="Bookkeepr.Application.IDomain"/>
    /// </summary>
    public sealed class Domain : IDomain
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Domain"/> class.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        /// <param name="dataSource">The data source.</param>
        public Domain(ILifetimeScope container, IDataSource dataSource)
        {
            Contract.Requires(container != null);
            Contract.Requires(dataSource != null);

            Container = container.BeginLifetimeScope();

            var builder = new ContainerBuilder();
            builder.RegisterInstance(dataSource);
            builder.Register(_ => dataSource.CreateRepository());

            builder.Update(Container.ComponentRegistry);
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(Container != null);
        }

        #region IDomain Members

        /// <summary>
        /// The IoC container associated with this domain
        /// </summary>
        public ILifetimeScope Container { get; private set; }

        /// <summary>
        /// Executes a command against this domain
        /// </summary>
        /// <param name="command">The command specification to execute</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void Execute(ICommand command)
        {
            ICommandHandler handler;
            if (!Container.TryResolve(typeof(ICommandHandler<>).MakeGenericType(command.GetType()), out handler) || handler == null)
            {
                throw new InvalidOperationException("Could not find command handler for {0}".FormatInvariant(command.GetType().FullName));
            }

            try
            {
                handler.Execute(command);
            }
            finally
            {
                var disposable = handler as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes a query against this domain
        /// </summary>
        /// <typeparam name="TResult">The return type of the query</typeparam>
        /// <param name="query">The query specification to execute</param>
        /// <returns>
        /// The result of the query operation
        /// </returns>
        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            IQueryHandler<TResult> handler;
            if (!Container.TryResolve(typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult)), out handler) || handler == null)
            {
                throw new InvalidOperationException("Could not find query handler for {0}".FormatInvariant(query.GetType().FullName));
            }

            TResult result;
            try
            {
                result = handler.Execute(query);
            }
            finally
            {
                var disposable = handler as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            return result;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Container.Dispose();
        }

        #endregion
    }
}
