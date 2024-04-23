namespace SSToolkit.Domain.Repositories.Model
{
    public interface IEntity<T> : IEntity
    {
        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        new T Id { get; set; }
    }

    public interface IEntity
    {
        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        object Id { get; set; }
    }
}
