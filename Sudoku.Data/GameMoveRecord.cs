
namespace Sudoku.Data
{
    public sealed class GameMoveRecord : Record
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int? Value { get; set; }
    }
}
