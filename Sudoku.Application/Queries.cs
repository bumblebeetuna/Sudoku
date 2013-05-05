using System;
using System.Collections.Generic;
using Sudoku.Domain;

namespace Sudoku.Application
{
    public sealed class FindGames : IQuery<IEnumerable<GameInfo>> { }

    public sealed class LoadGame : IQuery<Game>
    {
        public LoadGame(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
