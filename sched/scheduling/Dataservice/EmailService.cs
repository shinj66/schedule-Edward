using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Dataservice
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendNotification(string recipientEmail, string emailSubject, string bodyText)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    _configuration["EmailSettings:FromName"] ?? "Schedule System",
                    _configuration["EmailSettings:FromEmail"] ?? "no-reply@schedule.com"
                ));
                message.To.Add(new MailboxAddress("Student", recipientEmail));
                message.Subject = emailSubject;
                message.Body = new TextPart("plain") { Text = bodyText };

                using (var client = new SmtpClient())
                {
                    
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    int port = int.TryParse(_configuration["EmailSettings:SmtpPort"], out int p) ? p : 2525;
                    string host = _configuration["EmailSettings:SmtpHost"] ?? "sandbox.smtp.mailtrap.io";

                    client.Connect(host, port, SecureSocketOptions.StartTls);
                    client.Authenticate(
                        _configuration["EmailSettings:Username"],
                        _configuration["EmailSettings:Password"]
                    );

                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Mailtrap] Notification email sent to {recipientEmail}!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Mailtrap Warning] Email failed: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}