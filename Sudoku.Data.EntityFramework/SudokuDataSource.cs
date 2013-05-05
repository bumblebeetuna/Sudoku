using System.Diagnostics.Contracts;
using Autofac;
using Sudoku.Application;

namespace Sudoku.Data.EntityFramework
{
    public sealed class SudokuDataSource : IDataSource
    {
        private readonly ILifetimeScope container;

        public SudokuDataSource(ILifetimeScope container)
        {
            Contract.Requires(container != null);

            this.container = container;
        }

        #region IDataSource Members

        public IRepository CreateRepository()
        {
            var context = container.Resolve<SudokuDbContext>();

            Contract.Assume(context != null);

            return new EntityFrameworkRepository(context, container);
        }

        #endregion
    }
}
