using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sudoku.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("({X},{Y}): {Value}")]
    public sealed class GameMove : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameMove"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="value">The value.</param>
        public GameMove(Guid id, int x, int y, int? value, bool generated = false)
        {
            Contract.Requires(x >= 0 && x < 9);
            Contract.Requires(y >= 0 && y < 9);
            Contract.Requires(value >= 1 && value < 9);

            Id = id;
            X = x;
            Y = y;
            Value = value;
            Generated = generated;
        }

        /// <summary>
        /// Gets the X.
        /// </summary>
        /// <value>
        /// The X.
        /// </value>
        public int X { get; private set; }
        /// <summary>
        /// Gets the Y.
        /// </summary>
        /// <value>
        /// The Y.
        /// </value>
        public int Y { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int? Value { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="GameMove"/> is generated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if generated; otherwise, <c>false</c>.
        /// </value>
        public bool Generated { get; private set; }

        /// <summary>
        /// Possibles the values.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<int> PossibleValues(Game game)
        {
            Contract.Requires(game != null);
            Contract.Ensures(Contract.Result<IEnumerable<int>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<int>>().All(x => x >= 1 && x <= 9));

            var blockIndex = X / 3 + Y / 3 * 3;
            var allValues = Enumerable.Range(1, 9).ToList();

            var row = game.GetRow(Y);
            var col = game.GetColumn(X);
            var block = game.GetBlock(blockIndex);

            foreach (var value in row.Where(x => x.Value.HasValue).Select(x => x.Value.Value))
            {
                allValues.Remove(value);
            }

            foreach (var value in col.Where(x => x.Value.HasValue).Select(x => x.Value.Value))
            {
                allValues.Remove(value);
            }

            foreach (var value in block.Where(x => x.Value.HasValue).Select(x => x.Value.Value))
            {
                allValues.Remove(value);
            }

            return allValues.AsReadOnly();
        }
    }
}
