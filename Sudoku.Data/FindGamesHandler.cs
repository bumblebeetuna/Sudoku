using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Sudoku.Application;
using Sudoku.Domain;

namespace Sudoku.Data
{
    public sealed class FindGamesHandler : QueryHandler<FindGames, IEnumerable<GameInfo>>
    {
        private readonly IRepository repo;

        public FindGamesHandler(IRepository repo)
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

        public override IEnumerable<GameInfo> Execute(FindGames query)
        {
            return repo.Query<GameRecord>()
                .Select(x => new { x.Id, x.CreatedOn, PercentageComplete = x.Moves.Count(y => y.Value.HasValue) / x.Moves.Count() })
                .ToList()
                .Select(x => new GameInfo(x.Id, x.CreatedOn, x.PercentageComplete))
                .ToList();
        }
    }
}
