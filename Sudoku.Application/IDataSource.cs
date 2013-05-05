
namespace Sudoku.Application
{
    /// <summary>
    /// Represents a resource which a repository can query and write to
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Creates a repository against the underlying data store represented by this data source.
        /// </summary>
        /// <returns>A repository</returns>
        IRepository CreateRepository();
    }
}
