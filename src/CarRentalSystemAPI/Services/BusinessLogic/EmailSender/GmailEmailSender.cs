﻿namespace WebAPI.Services.BusinessLogic.EmailSender
{
    using MailKit.Net.Smtp;
    using MimeKit;

    using WebAPI.Common;
    using WebAPI.DTOs.Gmail;

    public class GmailEmailSender : IEmailSenderService
    {
        private readonly GmailSenderCofigKeys cofigKeys;

        public GmailEmailSender(GmailSenderCofigKeys cofigKeys)
        {
            this.cofigKeys = cofigKeys;
        }

        public void SendEmail(string to, string subject, string htmlContent)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(GlobalConstants.SystemName, this.cofigKeys.Email));
            mailMessage.To.Add(new MailboxAddress("User", to));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("html")
            {
                Text = htmlContent,
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate(this.cofigKeys.Email, this.cofigKeys.Password);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}
