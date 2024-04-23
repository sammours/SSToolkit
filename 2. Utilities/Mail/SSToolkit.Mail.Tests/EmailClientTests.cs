namespace SSToolkit.Mail.Tests
{
    using System;
    using System.Net.Mail;
    using Xunit;

    public class EmailClientTests
    {
        private EmailClient emailClient = new(string.Empty, string.Empty);

        [Fact]
        public void EmailClient_Test()
        {
            this.ExceptionAssertion<ArgumentNullException>("SmtpServer cannot be null",
                () => this.emailClient.SendEmailAsync("from", "to", "subject", "body").Wait());

            this.emailClient = new EmailClient("SMTP", string.Empty);
            this.ExceptionAssertion<ArgumentException>("SmtpPort is not valid",
                () => this.emailClient.SendEmailAsync("from", "to", "subject", "body").Wait());

            this.emailClient = new EmailClient("SMTP", "Not valid");
            this.ExceptionAssertion<ArgumentException>("SmtpPort is not valid",
                () => this.emailClient.SendEmailAsync("from", "to", "subject", "body").Wait());

            this.emailClient = new EmailClient("SMTP", "356");
            this.ExceptionAssertion<ArgumentException>("From Email ['from'] is not valid",
                () => this.emailClient.SendEmailAsync("from", "to", "subject", "body").Wait());

            this.ExceptionAssertion<ArgumentException>("At least one email should be provided as receivers",
                () => this.emailClient.SendEmailAsync("validfrom@mail.com", string.Empty, "subject", "body").Wait());

            this.ExceptionAssertion<ArgumentException>("CC email ['invalid'] is not valid",
                () => this.emailClient.SendEmailAsync("validfrom@mail.com", "validto@mail.com", "subject", "body", cc: new[] { "invalid" }).Wait());

            this.emailClient = new EmailClient("SMTP", "356", string.Empty, string.Empty);
            this.ExceptionAssertion<ArgumentNullException>("SmtpUsername cannot be null",
                () => this.emailClient.SendEmailAsync("from", "to", "subject", "body").Wait());

            this.emailClient = new EmailClient("SMTP", "356", "username", string.Empty);
            this.ExceptionAssertion<ArgumentNullException>("SmtpPassword cannot be null",
                () => this.emailClient.SendEmailAsync("from", "to", "subject", "body").Wait());

            this.emailClient = new EmailClient("SMTP", "356", "username", "password");
            // Valid, should throw System.Net.Mail.SmtpException due to the invalid input data
            this.ExceptionAssertion<SmtpException>("Failure sending mail",
                () => this.emailClient.SendEmailAsync("validfrom@mail.com", "validto@mail.com", "subject", "body",
                allowHtml: true, cc: new[] { "validcc@mail.com" }, null).Wait());

            this.ExceptionAssertion<SmtpException>("Failure sending mail",
                () => this.emailClient.SendEmailAsync("validfrom@mail.com", "validto@mail.com", "subject", "body",
                allowHtml: true, cc: new[] { "validcc@mail.com" }, new Attachment[] { }).Wait());
        }

        private void ExceptionAssertion<T>(string errorMessage, Action delg)
            where T : Exception
        {
            var ex = Assert.Throws<AggregateException>(delg);
            Assert.NotNull(ex);
            Assert.NotNull(ex.InnerException);
            if (ex.InnerException is not null)
            {
                Assert.Equal(typeof(T), ex.InnerException.GetType());
                Assert.StartsWith(errorMessage, ((T)ex.InnerException).Message);
            }
        }
    }
}