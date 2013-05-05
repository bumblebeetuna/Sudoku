using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using Autofac;
using Sudoku.Application;
using Sudoku.Infrastructure;

namespace Sudoku.Data.EntityFramework
{
    public class EntityFrameworkRepository : IRepository
    {
        private readonly DbContext context;
        private readonly ILifetimeScope container;

        public EntityFrameworkRepository(DbContext context, ILifetimeScope container)
        {
            Contract.Requires(context != null);
            Contract.Requires(container != null);

            this.context = context;
            this.container = container;
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(context != null);
            Contract.Invariant(container != null);
        }

        #region IRepository Members

        public IQueryable<T> Query<T>() where T : class
        {
            var set = context.Set<T>();

            if (set == null)
            {
                throw new ArgumentException("Cannot query over type {0}".FormatInvariant(typeof(T).FullName));
            }

            var strategy = container.ResolveOptional<IQueryStrategy<T>>();

            if (strategy != null)
            {
                return strategy.Apply(set);
            }

            return set;
        }

        public void Add(object entity)
        {
            var set = context.Set(entity.GetType());

            if (set == null)
            {
                throw new ArgumentException("Cannot add object of type {0}".FormatInvariant(entity.GetType().FullName), Member.Name(() => entity));
            }

            set.Add(entity);

            context.SaveChanges();
        }

        public void Delete(object entity)
        {
            var set = context.Set(entity.GetType());

            if (set == null)
            {
                throw new ArgumentException("Cannot delete object of type {0}".FormatInvariant(entity.GetType().FullName), Member.Name(() => entity));
            }

            set.Remove(entity);

            context.SaveChanges();
        }

        public void Update(object entity)
        {
            var set = context.Set(entity.GetType());

            if (set == null)
            {
                throw new ArgumentException("Cannot update object of type {0}".FormatInvariant(entity.GetType().FullName), Member.Name(() => entity));
            }

            set.Attach(entity);

            var entry = context.Entry(entity);
            Contract.Assume(entry != null);

            entry.State = EntityState.Modified;

            context.SaveChanges();
        }

        public void Create()
        {
            context.Database.CreateIfNotExists();
        }

        #endregion
    }

    [ContractClass(typeof(QueryStrategyContract<>))]
    public interface IQueryStrategy<T>
    {
        IQueryable<T> Apply(IQueryable<T> queryable);
    }

    #region Contracts

    [ContractClassFor(typeof(IQueryStrategy<>))]
    abstract class QueryStrategyContract<T> : IQueryStrategy<T>
    {
        #region IQueryStrategy<T> Members

        public IQueryable<T> Apply(IQueryable<T> queryable)
        {
            Contract.Requires(queryable != null);
            Contract.Ensures(Contract.Result<IQueryable<T>>() != null);

            return default(IQueryable<T>);
        }

        #endregion
    }

    #endregion

    public sealed class GameRecordQueryStrategy : IQueryStrategy<GameRecord>
    {
        #region IQueryStrategy<UserRecord> Members

        public IQueryable<GameRecord> Apply(IQueryable<GameRecord> queryable)
        {
            var query = queryable.Include(x => x.Moves);

            if (query == null)
            {
                throw new ArgumentException("Cannot query over type {0}".FormatInvariant(typeof(GameRecord).FullName));
            }

            return query;
        }

        #endregion
    }
}
