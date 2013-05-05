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

            var engine = new DancingLinksEngine();
            var generatedGame = engine.GenerateOne(7);

            var gameString = generatedGame.StringRep.Replace("\n", "").Replace("\r", "");

            for (var i = 0; i < gameString.Length; ++i)
            {
                if (gameString[i] != '.')
                {
                    gameRecord.Moves[i].Value = int.Parse(gameString[i].ToString());
                    gameRecord.Moves[i].Generated = true;
                }
            }

            repo.Add(gameRecord);
        }
    }
}
