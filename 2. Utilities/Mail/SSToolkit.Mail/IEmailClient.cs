namespace SSToolkit.Mail
{
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public interface IEmailClient
    {
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
        Task SendEmailAsync(string receiver, string subject, string body);

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
        Task SendEmailAsync(string sender, string receiver, string subject, string body);

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
        Task SendEmailAsync(string sender, string receiver, string subject, string body,
            bool allowHtml = false, string[]? cc = null, Attachment[]? attachmets = null);

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
        Task SafeSendEmailAsync(string sender, string receiver, string subject, string body, string[]? cc = null);

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
        Task SendEmailAsync(string sender, string[] receivers, string subject, string body,
            bool allowHtml = false, string[]? cc = null, Attachment[]? attachmets = null);
    }
}
