namespace SSToolkit.Mail
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailClient : IEmailClient
    {
        public EmailClient(EmailClientOptions options)
        {
            this.Options = options ?? throw new ArgumentNullException("Options cannot be null");
        }

        public EmailClient(string smtpServer, string smtpPort)
        {
            this.Options = new EmailClientOptions
            {
                SmtpServer = smtpServer,
                SmtpPort = smtpPort,
                UseDefaultCredentials = true
            };
        }

        public EmailClient(string smtpServer, string smtpPort, string smtpUsername, string smtpPassword)
        {
            this.Options = new EmailClientOptions
            {
                SmtpServer = smtpServer,
                SmtpPort = smtpPort,
                SmtpUsername = smtpUsername,
                SmtpPassword = smtpPassword,
            };
        }

        private EmailClientOptions Options { get; set; }

        /// <summary>
        /// Send email to <paramref name="receiver"/>
        /// The sender will be the one which provided in the options <see cref="EmailClientOptions.MailSender"/>
        /// </summary>
        /// <param name="receiver">The receiver email.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        /// <exception cref="Exception">Exception</exception>
        public async Task SendEmailAsync(string receiver, string subject, string body)
        {
            if (this.Options.MailSender is not null)
            {
                await this.SendEmailAsync(this.Options.MailSender, new[] { receiver }, subject, body).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Send email to <paramref name="receiver"/>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="receiver">The receiver email.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        /// <exception cref="Exception">Exception</exception>
        public async Task SendEmailAsync(string sender, string receiver, string subject, string body)
            => await this.SendEmailAsync(sender, new[] { receiver }, subject, body).ConfigureAwait(false);

        /// <summary>
        /// Send email to <paramref name="receiver"/>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="receiver">The receiver email.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <param name="allowHtml">Email should include HTML (default: false)</param>
        /// <param name="cc">List of CC (default: null)</param>
        /// <param name="attachmets">List of attachments (default: null)</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        /// <exception cref="Exception">Exception</exception>
        public async Task SendEmailAsync(string sender, string receiver, string subject, string body,
            bool allowHtml = false, string[]? cc = null, Attachment[]? attachmets = null)
            => await this.SendEmailAsync(sender, new[] { receiver }, subject, body, allowHtml, cc, attachmets).ConfigureAwait(false);

        /// <summary>
        /// Send email to <paramref name="receiver"/>
        /// Email will contain neither HTML nor attachments
        /// Both subject and Body will be HTML stripped
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="receiver">The receiver email.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <param name="cc">List of CC (default: null)</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        /// <exception cref="Exception">Exception</exception>
        public async Task SafeSendEmailAsync(string sender, string receiver, string subject, string body, string[]? cc = null)
            => await this.SendEmailAsync(sender, new[] { receiver }, subject.StripHTML(), body.StripHTML(), false, cc, null).ConfigureAwait(false);

        /// <summary>
        /// Send email to list of <paramref name="receivers"/>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="receivers">List of receivers.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <param name="allowHtml">Email should include HTML (default: false)</param>
        /// <param name="cc">List of CC (default: null)</param>
        /// <param name="attachmets">List of attachments (default: null)</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        /// <exception cref="Exception">Exception</exception>
        public async Task SendEmailAsync(string sender, string[] receivers, string subject, string body,
            bool allowHtml = false, string[]? cc = null, Attachment[]? attachmets = null)
        {
            var client = this.CreateSmtpClient();

            if (!this.IsValidEmail(sender))
            {
                throw new ArgumentException($"From Email ['{sender}'] is not valid", nameof(sender));
            }

            if (receivers == null || receivers.Length == 0 || receivers.All(receiver => string.IsNullOrEmpty(receiver)))
            {
                throw new ArgumentException("At least one email should be provided as receivers", nameof(receivers));
            }

            var message = new MailMessage
            {
                From = new MailAddress(sender.Trim()),
                Body = body,
                Subject = subject,
                IsBodyHtml = allowHtml
            };

            foreach (var email in receivers)
            {
                if (!this.IsValidEmail(email))
                {
                    throw new ArgumentException($"Receiver email ['{email}'] is not valid", nameof(email));
                }

                message.To.Add(email);
            }

            this.AddCCIfNotNull(message, cc);
            AddAttachmentsIfNotNull(message, attachmets);

            try
            {
                await client.SendMailAsync(message).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        private static void AddAttachmentsIfNotNull(MailMessage message, Attachment[]? attachmets)
        {
            if (attachmets != null)
            {
                foreach (var attachment in attachmets)
                {
                    message.Attachments.Add(attachment);
                }
            }
        }

        private void AddCCIfNotNull(MailMessage message, string[]? cc)
        {
            if (cc != null)
            {
                foreach (var email in cc)
                {
                    if (!this.IsValidEmail(email))
                    {
                        throw new ArgumentException($"CC email ['{email}'] is not valid", nameof(email));
                    }

                    message.CC.Add(email);
                }
            }
        }

        private SmtpClient CreateSmtpClient()
        {
            if (string.IsNullOrEmpty(this.Options.SmtpServer))
            {
                throw new ArgumentNullException(nameof(this.Options.SmtpServer), "SmtpServer cannot be null");
            }

            if (!int.TryParse(this.Options.SmtpPort, NumberStyles.Integer, CultureInfo.InvariantCulture, out int port))
            {
                throw new ArgumentException("SmtpPort is not valid", nameof(this.Options.SmtpPort));
            }

            var client = new SmtpClient(this.Options.SmtpServer, port)
            {
                EnableSsl = this.Options.EnableSsl
            };

            if (this.Options.UseDefaultCredentials)
            {
                client.UseDefaultCredentials = true;
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            else
            {
                if (string.IsNullOrEmpty(this.Options.SmtpUsername))
                {
                    throw new ArgumentNullException(nameof(this.Options.SmtpUsername), "SmtpUsername cannot be null");
                }

                if (string.IsNullOrEmpty(this.Options.SmtpPassword))
                {
                    throw new ArgumentNullException(nameof(this.Options.SmtpPassword), "SmtpPassword cannot be null");
                }

                client.UseDefaultCredentials = false;
                client.EnableSsl = this.Options.EnableSsl;
                client.Credentials = new NetworkCredential(this.Options.SmtpUsername, this.Options.SmtpPassword);
            }

            return client;
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                return new MailAddress(email).Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
