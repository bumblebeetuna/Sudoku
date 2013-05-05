using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sudoku.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Game : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="moves">The moves.</param>
        public Game(Guid id, IEnumerable<GameMove> moves)
        {
            Id = id;
            Moves = moves.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the moves.
        /// </summary>
        /// <value>
        /// The moves.
        /// </value>
        public IList<GameMove> Moves { get; private set; }

        public bool IsComplete
        {
            get
            {
                return Moves.All(x => x.Value.HasValue);
            }
        }

        /// <summary>
        /// Gets the block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<GameMove> GetBlock(int block)
        {
            Contract.Requires(block >= 0 && block < 9);
            Contract.Ensures(Contract.Result<IEnumerable<GameMove>>().All(x => x != null));
            Contract.Ensures(Contract.Result<IEnumerable<GameMove>>() != null);

            return Moves
                .Where(x => x.X / 3 + x.Y / 3 * 3 == block)
                .Take(9)
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public IEnumerable<GameMove> GetRow(int row)
        {
            Contract.Requires(row >= 0 && row < 9);
            Contract.Ensures(Contract.Result<IEnumerable<GameMove>>().All(x => x != null));
            Contract.Ensures(Contract.Result<IEnumerable<GameMove>>() != null);

            return Moves
                .OrderBy(x => x.Y)
                .ThenBy(x => x.X)
                .Skip(row * 9)
                .Take(9)
                .OrderBy(x => x.X)
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public IEnumerable<GameMove> GetColumn(int column)
        {
            Contract.Requires(column >= 0 && column < 9);
            Contract.Ensures(Contract.Result<IEnumerable<GameMove>>().All(x => x != null));
            Contract.Ensures(Contract.Result<IEnumerable<GameMove>>() != null);

            return Moves
                .Where(x => x.X == column)
                .Take(9)
                .OrderBy(x => x.Y)
                .ToList()
                .AsReadOnly();
        }
    }
}
