# SSToolkit.Mail
Mail helper project that provides a tested Emailing client

> Install-Package SSToolkit.Mail

## EmailClient

How to use:
```csharp
var emailClient = new EmailClient("[SmtpServer]", "[SmtpPort]");
await emailClient.SendEmailAsync("[from]", "[to]", "[subject]", "[body]");
```

Other constructors:
```csharp
EmailClient("[SmtpServer]", "[SmtpPort]", "[SmtpUsername]", "[SmtpPassword]");
```
```csharp
var options = new EmailClientOptions
{
	SmtpServer = "[SmtpServer]",
    SmtpPort = "[SmtpPort]",
    SmtpUsername = "[SmtpUsername]" ,
    SmtpPassword = "[SmtpPassword]"
};
EmailClient(options);
```

Methods:
```csharp
// The sender is provided in the option (MailSender). Suitable for Emails from your info Email.
Task SendEmailAsync(string receiver, string subject, string body);
```
```csharp
Task SendEmailAsync(string sender, string receiver, string subject, string body);
```
```csharp
Task SendEmailAsync(string sender, string receiver, string subject, string body,
            bool allowHtml = false, string[] cc = null, Attachment[] attachmets = null);
```
```csharp
// Removes HTML tags from both subject and body
Task SafeSendEmailAsync(string sender, string receiver, string subject, string body, string[] cc = null);
```
```csharp
Task SendEmailAsync(string sender, string[] receivers, string subject, string body,
        bool allowHtml = false, string[] cc = null, Attachment[] attachmets = null);
```

## The EmailClientOptions

EmailClientOptions

```csharp
public class EmailClientOptions
{
    public string SmtpServer { get; set; }

    public string SmtpPort { get; set; }

    public string SmtpUsername { get; set; }

    public string SmtpPassword { get; set; }

    public string MailSender { get; set; }

    public bool UseDefaultCredentials { get; set; } = false;

    public bool EnableSsl { get; set; } = false;
}
```

## StripHTML

StripHTML extension

```csharp
public static string StripHTML(this string input)
{
    return Regex.Replace(input, "<[\\/a-zA-Z0-9= \"\\\"'\'#;:()$_-]*?>", string.Empty);
}
```

