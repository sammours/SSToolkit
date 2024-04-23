namespace SSToolkit.Domain.Repositories.Model
{
    public interface IStateEntity
    {
        /// <summary>
        /// Gets the state for this instance.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public State State { get; }
    }
}
