namespace ETMS.Repository.Helpers;

using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

public static class EmailHelper
{
    public static async Task SendHtmlEmailAsync(string toEmail, string subject, string htmlBody, string userName, string server, string password, int port, string fromEmail)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(fromEmail)); // update!
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = htmlBody
        };
        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        // Configure your SMTP host, SSL, and credentials here
        await smtp.ConnectAsync(server, port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(userName, password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}