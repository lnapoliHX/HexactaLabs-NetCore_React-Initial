namespace Stock.Model.Base
{
    /// <summary>
    /// Interface for entities whose name name must be unique.
    /// </summary>
    public interface IUniquelyNamedEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        public string Name { get; set; }
    }
}
