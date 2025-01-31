﻿
using Microsoft.Extensions.Options;
using NetCoreIdentityBlogApp.Models.OptionsModels;
using System.Net;
using System.Net.Mail;

namespace NetCoreIdentityBlogApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings=options.Value;
        }

        public async Task SendResetPasswordEmail(string resetPasswordEmailLink, string toEmail)
        {
            var smptClient = new SmtpClient();
            smptClient.Host = _emailSettings.Host;
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email,_emailSettings.Password);
            smptClient.EnableSsl = true;

            var mailMessage = new MailMessage();
             mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Localhost : Şifre Sıfırlama Linki";
            mailMessage.Body = $@"
                                <h4>Şifrenizi yenilemek için aşağıdaki linki tıklayınız. </h4>
                               <p><a href='{resetPasswordEmailLink}'> Şifre yenileme linki</a> </p>
                               ";
            mailMessage.IsBodyHtml = true;
            await smptClient.SendMailAsync(mailMessage);


        }
    }
}
