using System;

namespace Sudoku.Domain
{
    /// <summary>
    /// Base class for all entities. An entity has an identity represented by a <see cref="System.Guid"/> value
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the id of the entity.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; protected set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity;

            if (entity != null)
            {
                return entity.Id == Id;
            }

            return base.Equals(obj);
        }
    }
}
