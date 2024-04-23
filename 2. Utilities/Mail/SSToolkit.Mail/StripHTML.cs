namespace SSToolkit.Mail
{
    using System.Text.RegularExpressions;

    public static partial class MailExtensions
    {
        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<[\\/a-zA-Z0-9= \"\\\"'\'#;:()$_-]*?>", string.Empty);
        }
    }
}
