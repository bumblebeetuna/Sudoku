using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Sudoku.Application;

namespace Sudoku.Data
{
    public sealed class CreateGameHandler : CommandHandler<CreateGame>
    {
        private readonly IRepository repo;

        public CreateGameHandler(IRepository repo)
        {
            Contract.Requires(repo != null);

            this.repo = repo;
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(repo != null);
        }

        public override void Execute(CreateGame command)
        {
            if (repo.Query<GameRecord>().Any(x => x.Id == command.Id))
            {
                throw new DuplicateEntityException();
            }

            var gameRecord = new GameRecord
            {
                Id = command.Id,
                Moves = Enumerable.Range(0, 9 * 9).Select(y => new GameMoveRecord
                {
                    Id = Guid.NewGuid(),
                    X = y % 9,
                    Y = y / 9,
                }).ToList(),
            };

            var random = new Random();

            foreach (var i in Enumerable.Range(0, 27))
            {
                var game = LoadGameHandler.Create(gameRecord);
                var emptyMoves = game.Moves.Where(x => !x.Value.HasValue).ToList();
                var gameMove = emptyMoves[random.Next(emptyMoves.Count)];
                for (; !gameMove.PossibleValues(game).Any(); gameMove = game.Moves[random.Next(9 * 9)]) ;

                var gameMoveRecord = gameRecord.Moves.First(x => x.Id == gameMove.Id);
                var possibleValues = gameMove.PossibleValues(game).ToList();
                gameMoveRecord.Value = possibleValues[random.Next(possibleValues.Count)];
                gameMoveRecord.Generated = true;
            }

            repo.Add(gameRecord);
        }
    }
}
