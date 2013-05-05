using System.Diagnostics.Contracts;
using System.Linq;

namespace Sudoku.Application
{
    /// <summary>
    /// Represents a generic repository for CRUD operations on a datasource
    /// </summary>
    [ContractClass(typeof(RepositoryContract))]
    public interface IRepository
    {
        /// <summary>
        /// Queries the datasource
        /// </summary>
        /// <typeparam name="T">The type of object to query</typeparam>
        /// <returns></returns>
        IQueryable<T> Query<T>() where T : class;

        /// <summary>
        /// Adds an object to the datasource
        /// </summary>
        /// <param name="entity">The object to add</param>
        /// <exception cref="Bookkeepr.Application.DuplicateEntityException">If the object already exists in the datasource</exception>
        void Add(object entity);
        /// <summary>
        /// Removes an object from the datasource
        /// </summary>
        /// <param name="entity">The object to remove. The object must already exist</param>
        /// <exception cref="Bookkeepr.Application.EntityNotFoundException">If the object does not exist in the datasource</exception>
        void Delete(object entity);
        /// <summary>
        /// Updates an existing entity in the datasource
        /// </summary>
        /// <param name="entity">The object to update. The object must already exist</param>
        /// <exception cref="Bookkeepr.Application.EntityNotFoundException">If the object does not exist in the datasource</exception>
        void Update(object entity);

        /// <summary>
        /// Creates the backing store for this repository.
        /// </summary>
        void Create();
    }

    #region Contracts

    [ContractClassFor(typeof(IRepository))]
    abstract class RepositoryContract : IRepository
    {
        #region IRepository Members

        public IQueryable<T> Query<T>() where T : class
        {
            Contract.Ensures(Contract.Result<IQueryable<T>>() != null);

            return default(IQueryable<T>);
        }

        public void Add(object entity)
        {
            Contract.Requires(entity != null);
        }

        public void Delete(object entity)
        {
            Contract.Requires(entity != null);
        }

        public void Update(object entity)
        {
            Contract.Requires(entity != null);
        }

        public void Create()
        {

        }

        #endregion
    }

    #endregion
}
