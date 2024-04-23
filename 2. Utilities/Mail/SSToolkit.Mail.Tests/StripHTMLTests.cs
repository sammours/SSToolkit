namespace SSToolkit.Mail.Tests
{
    using Shouldly;
    using Xunit;

    public class StripHTMLTests
    {
        [Fact]
        public void StripHTML_Tests()
        {
            "<>".StripHTML().ShouldBeEmpty();
            "<b>".StripHTML().ShouldBeEmpty();
            "<b></b>".StripHTML().ShouldBeEmpty();
            "</b>".StripHTML().ShouldBeEmpty();
            "<font></font>".StripHTML().ShouldBeEmpty();
            "<font class=\"some\"></font>".StripHTML().ShouldBeEmpty();
            "<font style=\"background: #fff;\"></font>".StripHTML().ShouldBeEmpty();
            "<font style=\"background: red;\"></font>".StripHTML().ShouldBeEmpty();
            "<font style=\"background-color: red;\"></font>".StripHTML().ShouldBeEmpty();
            "<font style=\"background-color: $red;\"></font>".StripHTML().ShouldBeEmpty();
            "<font data-type=\"some\"></font>".StripHTML().ShouldBeEmpty();
            "<font data-type='some'></font>".StripHTML().ShouldBeEmpty();
            "<font data_type=\"some\"></font>".StripHTML().ShouldBeEmpty();
            "<font data_type='some'></font>".StripHTML().ShouldBeEmpty();
            "<font style=\"width: calc(1);\"></font>".StripHTML().ShouldBeEmpty();
            "<font style='width: calc(1);'></font>".StripHTML().ShouldBeEmpty();
            "before <font style='width: calc(1);'>middle</font> after".StripHTML().ShouldBe("before middle after");
            "7 < 10 <b>but</b> 30 > 10' it gives: '7 but 30 > 10".StripHTML().ShouldBe("7 < 10 but 30 > 10' it gives: '7 but 30 > 10");
        }
    }
}