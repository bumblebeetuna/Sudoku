using System;
using System.Diagnostics.Contracts;

namespace Sudoku.Application
{
    public sealed class CreateGame : ICommand
    {
        public CreateGame(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public sealed class MakeMove : ICommand
    {
        public MakeMove(Guid gameId, int x, int y, int value)
        {
            Contract.Requires(x >= 0 && x < 9);
            Contract.Requires(y >= 0 && y < 9);
            Contract.Requires(value >= 1 && value <= 9);

            GameId = gameId;
            X = x;
            Y = y;
            Value = value;
        }

        public Guid GameId { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Value { get; private set; }
    }
}
