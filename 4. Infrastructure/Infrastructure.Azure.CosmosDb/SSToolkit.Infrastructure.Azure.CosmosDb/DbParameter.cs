namespace SSToolkit.Infrastructure.Azure.CosmosDb
{
    public class DbParameter
    {
        public DbParameter(string field, object value)
        {
            this.Value = value;
            this.Field = field;
        }

        public object Value { get; set; }

        public string Field { get; set; }
    }
}
