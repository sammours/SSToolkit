namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class SafeConcatTests
    {
        [Fact]
        public void SafeConcat_Test()
        {
            var listToAdd = new List<string> { "item" };
            var itemToAdd = "item";

            (null as IEnumerable<string>).SafeConcat(itemToAdd).ShouldBe(listToAdd);
            (null as IEnumerable<string>).SafeConcat(listToAdd).ShouldBe(listToAdd);

            ((IEnumerable<string>)new List<string> { }).SafeConcat(itemToAdd).ShouldBe(listToAdd);
            ((IEnumerable<string>)new List<string> { }).SafeConcat(listToAdd).ShouldBe(listToAdd);

            (null as IList<string>).SafeConcat(itemToAdd).ShouldBe(listToAdd);
            (null as IList<string>).SafeConcat(listToAdd).ShouldBe(listToAdd);

            ((IList<string>)new List<string> { }).SafeConcat(itemToAdd).ShouldBe(listToAdd);
            ((IList<string>)new List<string> { }).SafeConcat(listToAdd).ShouldBe(listToAdd);

            (null as string[]).SafeConcat(itemToAdd).ShouldBe(listToAdd);
            (null as string[]).SafeConcat(listToAdd).ShouldBe(listToAdd);

            new string[] { }.SafeConcat(itemToAdd).ShouldBe(listToAdd);
            new string[] { }.SafeConcat(listToAdd).ShouldBe(listToAdd);
        }
    }
}
