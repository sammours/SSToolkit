namespace SSToolkit.Domain.EventSourcing.Tests
{
    using System.Reflection;
    using SSToolkit.Domain.EventSourcing.Events;

    public class Testing
    {
        [Fact]
        public void Test()
        {
            var types = typeof(ProductPriceHasBeenChanged).Assembly.GetTypes().Where(
                    a => a.IsClass && a.IsSubclassOf(typeof(IAggregateEvent)))
                .ToList();
            foreach (var typ in types)
            {
                var props = typ.GetProperties(BindingFlags.DeclaredOnly |
                                              BindingFlags.Public |
                                              BindingFlags.Instance);
                foreach (var prop in props)
                {
                    if (prop.CanRead && !prop.CanWrite)
                    {
                        throw new Exception($"Klasse {typ.FullName} : Set-Property {prop.Name} is required .");
                    }
                }
            }
        }
    }
}
