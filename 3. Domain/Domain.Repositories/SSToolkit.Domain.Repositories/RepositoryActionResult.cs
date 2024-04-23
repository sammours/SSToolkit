namespace SSToolkit.Domain.Repositories
{
    using System.ComponentModel;

    public enum RepositoryActionResult
    {
        [Description("no entity action")]
        None,

        [Description("entity inserted")]
        Inserted,

        [Description("entity updated")]
        Updated,

        [Description("entity deleted")]
        Deleted
    }
}
