
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SmartMeterLibServices.Configurations;
using System;
using System.Threading.Tasks;

namespace SmartMeterLibServices.ExternalServices
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Execute(Options.SendGridKey, subject, message, email);
        }
        //private async Task Execute(string sendGridKey, string subject, string message, string email)
        //{


        //    MimeMessage msg = new MimeMessage();

        //    MailboxAddress from = new MailboxAddress("Admin", "pelegocoded@gmail.com");
        //    msg.From.Add(from);

        //    MailboxAddress to = new MailboxAddress("User", "michealpelumi48@yahoo.com");
        //    msg.To.Add(to);

        //    msg.Subject = subject;


        //    BodyBuilder bodyBuilder = new BodyBuilder();
        //    bodyBuilder.HtmlBody = message;
        //    msg.Body = bodyBuilder.ToMessageBody();


        //    SmtpClient client = new SmtpClient();
        //    await client.ConnectAsync("smtp.gmail.com", 587, false);
        //    await client.AuthenticateAsync("pelegocoded@gmail.com", "Salami123..@@__");



        //    try
        //    {
        //        await client.SendAsync(msg);
        //        await client.DisconnectAsync(true);
        //        client.Dispose();
        //    }
        //    catch (Exception)
        //    {
        //        {
        //            await client.SendAsync(msg);
        //            await client.DisconnectAsync(true);
        //            client.Dispose();
        //        }


        //    }

        //}



        private async Task Execute(string sendGridKey, string subject, string message, string email)
        {


            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("pelegocoded@gmail.com", "SMART METER"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = $"<div>{message}</div>"
            };
            msg.AddTo(new EmailAddress(email, email));
            try
            {
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception)
            {

                var response = await client.SendEmailAsync(msg);
            }
           

        }


    }
}
