using System.Collections.Generic;

namespace Sudoku.Data
{
    public sealed class GameRecord : Record
    {
        public ICollection<GameMoveRecord> Moves { get; set; }
    }
}
