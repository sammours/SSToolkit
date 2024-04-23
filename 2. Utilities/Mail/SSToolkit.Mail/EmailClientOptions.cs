namespace SSToolkit.Mail
{
    public class EmailClientOptions
    {
        public string? SmtpServer { get; set; }

        public string? SmtpPort { get; set; }

        public string? SmtpUsername { get; set; }

        public string? SmtpPassword { get; set; }

        public string? MailSender { get; set; }

        public bool UseDefaultCredentials { get; set; } = false;

        public bool EnableSsl { get; set; } = false;
    }
}
