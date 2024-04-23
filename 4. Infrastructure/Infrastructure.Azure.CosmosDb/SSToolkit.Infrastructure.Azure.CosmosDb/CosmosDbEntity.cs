namespace SSToolkit.Infrastructure.Azure.CosmosDb
{
    using Newtonsoft.Json;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Fundamental;

    public class CosmosDbEntity : Entity<string>, IEntity<string>, IStateEntity
    {
        /// <summary>
        /// Gets the type of the entity (discriminator).
        /// </summary>
        /// <value>
        /// The type of the entity.
        /// </value>
        [JsonProperty(PropertyName = "_et")]
        public string Discriminator => this.GetType().FullPrettyName();

        public State State { get; private set; } = new State();
    }
}
