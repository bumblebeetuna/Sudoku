using System.Collections.Generic;

namespace Sudoku.Data
{
    public sealed class GameRecord : Record
    {
        public IList<GameMoveRecord> Moves { get; set; }
    }
}
