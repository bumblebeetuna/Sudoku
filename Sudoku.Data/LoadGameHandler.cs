using System.Diagnostics.Contracts;
using System.Linq;
using Sudoku.Application;
using Sudoku.Domain;

namespace Sudoku.Data
{
    public sealed class LoadGameHandler : QueryHandler<LoadGame, Game>
    {
        private readonly IRepository repo;

        public LoadGameHandler(IRepository repo)
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

        public override Game Execute(LoadGame query)
        {
            var gameRecord = repo.Query<GameRecord>()
                .FirstOrDefault(x => x.Id == query.Id);

            if (gameRecord == null)
            {
                throw new EntityNotFoundException();
            }

            return Create(gameRecord);
        }

        public static Game Create(GameRecord gameRecord)
        {
            return new Game(gameRecord.Id, gameRecord.Moves.Select(x => new GameMove(x.Id, x.X, x.Y, x.Value)));
        }
    }
}
