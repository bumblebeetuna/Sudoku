using System;

namespace Sudoku.Domain
{
    public sealed class GameInfo : Entity
    {
        public GameInfo(Guid id, DateTime createdOn, int percentageComplete)
        {
            Id = id;
            CreatedOn = createdOn;
            PercentageComplete = percentageComplete;
        }

        public DateTime CreatedOn { get; private set; }
        public int PercentageComplete { get; private set; }
    }
}
