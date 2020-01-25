﻿using Microsoft.Extensions.Configuration;

using MimeKit;
using MailKit.Net.Smtp;

namespace StomatoloskaOrdinacija.Web.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(IConfiguration configuration, string receiverName, string receiverEmail, string subject, string message)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(configuration.GetValue<string>("EmailSettings:Name"), 
                configuration.GetValue<string>("EmailSettings:Email")));
            email.To.Add(new MailboxAddress(receiverName, receiverEmail));

            email.Subject = subject;
            email.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect(configuration.GetValue<string>("SmtpSettings:ServerAddress"), 
                    int.Parse(configuration.GetValue<string>("SmtpSettings:Port")), false);
                client.Authenticate(configuration.GetValue<string>("EmailSettings:Email"), 
                    configuration.GetValue<string>("EmailSettings:Password"));
                client.Send(email);
            }
        }
    }
}