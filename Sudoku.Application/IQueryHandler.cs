using System.Diagnostics.Contracts;

namespace Sudoku.Application
{
    /// <summary>
    /// Interface for defining the execution logic of a query
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    [ContractClass(typeof(IQueryHandlerContract<>))]
    public interface IQueryHandler<TResult>
    {
        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The result of the query operation</returns>
        TResult Execute(IQuery<TResult> query);
    }

    /// <summary>
    /// Interface for defining the execution logic of a specific query
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    [ContractClass(typeof(IQueryHandlerContract<,>))]
    public interface IQueryHandler<in TQuery, TResult> : IQueryHandler<TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The result of the query operation</returns>
        TResult Execute(TQuery query);
    }

    /// <summary>
    /// Base class for defining the execution logic of a specific query
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    [ContractClass(typeof(QueryHandlerContract<,>))]
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        #region IQueryHandler<TQuery,TResult> Members

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public abstract TResult Execute(TQuery query);

        #endregion

        #region IQueryHandler<TResult> Members

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The result of the query operation</returns>
        public TResult Execute(IQuery<TResult> query)
        {
            return Execute((TQuery)query);
        }

        #endregion
    }

    #region Contracts

    [ContractClassFor(typeof(IQueryHandler<>))]
    abstract class IQueryHandlerContract<TResult> : IQueryHandler<TResult>
    {
        #region IQueryHandler<TResult> Members

        public TResult Execute(IQuery<TResult> query)
        {
            Contract.Requires(query != null);

            return default(TResult);
        }

        #endregion
    }

    [ContractClassFor(typeof(IQueryHandler<,>))]
    abstract class IQueryHandlerContract<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        #region IQueryHandler<TQuery,TResult> Members

        public TResult Execute(TQuery query)
        {
            Contract.Requires(query != null);

            return default(TResult);
        }

        #endregion

        #region IQueryHandler<TResult> Members

        public TResult Execute(IQuery<TResult> query)
        {
            //Contract.Requires(query != null);

            return default(TResult);
        }

        #endregion
    }

    [ContractClassFor(typeof(QueryHandler<,>))]
    abstract class QueryHandlerContract<TQuery, TResult> : QueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public override TResult Execute(TQuery query)
        {
            Contract.Requires(query != null);

            return default(TResult);
        }
    }

    #endregion
}
