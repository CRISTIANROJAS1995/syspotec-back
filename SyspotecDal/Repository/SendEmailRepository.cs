using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SendGrid;
//using sib_api_v3_sdk.Api;
//using sib_api_v3_sdk.Client;
//using sib_api_v3_sdk.Model;
using System;

namespace SyspotecDal.Repository
{
    public class SendEmailRepository : ISendEmailRepository
    {
        private readonly ApplicationDbContext _context;

        public SendEmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendEmailOtpAsync(string email, string code, string type)
        {
            string HtmlContent = "Recovery";
            if (type == "Recovery")
            {
                HtmlContent = "<html><body>" +
                "<p>Dear HiBeats member,<br>This is your code to reset your password <b>" + code + "</b></p>" +
                "</body></html>";
            }
             else
                HtmlContent = "<html><body>" +
                "<p>Your verification code is <b>" + code + "</b></p>" +
                "</body></html>";

            var client = new SendGridClient("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");
            var from = new EmailAddress("info@hibeats.com", "HiBeats");
            var subject = "Verification code";
            var to = new EmailAddress(email);
            var htmlContent = HtmlContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);

            try
            {
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                return false;
            }
         
            return true;
        }

        public bool SendEmailRecovery(string email, string guid)
        {
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
            //{
            //    Configuration.Default.ApiKey.Add("api-key", "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");
            //    Configuration.Default.ApiKey.Add("partner-key", "app-smtp");
            //}

            //var apiInstance = new TransactionalEmailsApi();

            //string SenderName = "Renta Inmobiliaria";
            //string SenderEmail = "cristianjoseroj@outlook.com";
            //SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);

            //string ToEmail = email;
            //SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, null);

            //List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            //To.Add(smtpEmailTo);

            //string HtmlContent = "<html><body>" +
            // "<p>Puede restablecer la contraseña con este enlace:</p>" +
            // "<p>http://simonrojas1-001-site1.atempurl.com/#/forgot-password?guid=" + guid + "</p>" +
            // "<p>Este enlace caducará en 24 horas.</p>" +
            // "</body></html>";

            //string TextContent = null;
            //string Subject = "Recuperación de Contraseña";

            //SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(ToEmail, null);
            //List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>();
            //To1.Add(smtpEmailTo1);

            //try
            //{
            //    var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, Subject, null, null, null, null, null, null, null);
            //    CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}

            return true;
        }

        public async Task<bool> SendEmailBuys(string email)
        {
            string HtmlContent = "<html><body>" +
                "<p>Congratulations! You have bought one of our gift cards!</p>" +
                "<p>We recognize your engagement in the community. Keep it up, hard work pays off!</p>" +
                "<p><b>The HiBeats Team</b></p>" +
                "</body></html>";

            var client = new SendGridClient("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");
            var from = new EmailAddress("info@hibeats.com", "HiBeats");
            var subject = "Congratulations on your purchase";
            var to = new EmailAddress("cristianjoseroj@gmail.com");
            var htmlContent = HtmlContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);

            try
            {
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
