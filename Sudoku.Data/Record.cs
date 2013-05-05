using System;

namespace Sudoku.Data
{
    public abstract class Record
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
