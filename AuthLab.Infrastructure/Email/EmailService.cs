using AuthLab.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AuthLab.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _options;

        public EmailService(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            MimeMessage email = new();

            email.From.Add(MailboxAddress.Parse(_options.From));

            email.To.Add(MailboxAddress.Parse(to));

            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using SmtpClient smtp = new();

            await smtp.ConnectAsync(
                _options.Host,
                _options.Port,
                _options.EnableSsl);

            await smtp.AuthenticateAsync(
                _options.Username,
                _options.Password);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}
