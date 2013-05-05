using System.Collections.Generic;

namespace Sudoku.Data
{
    public sealed class GameRecord : Record
    {
        public IEnumerable<GameMoveRecord> Moves { get; set; }
    }
}
