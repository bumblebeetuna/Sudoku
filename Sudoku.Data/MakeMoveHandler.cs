using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Sudoku.Application;

namespace Sudoku.Data
{
    public sealed class MakeMoveHandler : CommandHandler<MakeMove>
    {
        private readonly IRepository repo;

        public MakeMoveHandler(IRepository repo)
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

        public override void Execute(MakeMove command)
        {
            var moveRecord = repo.Query<GameRecord>()
                .Where(x => x.Id == command.GameId)
                .SelectMany(x => x.Moves)
                .FirstOrDefault(x => x.X == command.X && x.Y == command.Y);

            if (moveRecord == null)
            {
                throw new EntityNotFoundException();
            }

            if (moveRecord.Generated)
            {
                throw new InvalidOperationException("Cannot change a generated move's value");
            }

            moveRecord.Value = command.Value;

            repo.Update(moveRecord);
        }
    }
}
