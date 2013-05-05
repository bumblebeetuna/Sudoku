using System.Data.Entity;

namespace Sudoku.Data.EntityFramework
{
    public sealed class SudokuDbContext : DbContext
    {
        public SudokuDbContext()
            : base("SudokuConnection")
        {

        }

        public DbSet<GameRecord> Games { get; set; }
    }
}
