using System;

namespace Sudoku.Data
{
    public abstract class Record
    {
        public Record()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
