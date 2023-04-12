using BSMediator.Core.Services.Opreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Service.Services.Opreation
{
    public class EmailService : IEmailService
    {

        private readonly IAppSettingService _appSetting;


        public EmailService(IAppSettingService appSetting)
        {
            _appSetting = appSetting;
        }

        #region Send Email General
        public async Task<bool> SendEmailAsync(string to, string subject, string body, string cc, string bcc, List<Attachment> attachment, bool isBodyHtml = false)
        {
            #region Get Email Configurations
            var cofig =await  _appSetting.GetAppSettingMail();

            #endregion
            if (isBodyHtml)
            {
                body = body.Replace("\r\n", "<br/>");
            }
            SmtpClient mailClient = new SmtpClient(cofig.Host);
            mailClient.EnableSsl = true;// changed for // Server does not support secure connections.
            mailClient.Port = 587;
            mailClient.Credentials = new System.Net.NetworkCredential(cofig.mailForm, cofig.Password);
            // Create the mail message

            MailMessage mailMessage = new MailMessage(cofig.mailForm, to, subject, body);
            //foreach (Attachment item in attachment)
            //{
            //    mailMessage.Attachments.Add(item);

            //}
            if (!string.IsNullOrEmpty(cc))
                mailMessage.CC.Add(cc);

            if (!string.IsNullOrEmpty(bcc))
                mailMessage.Bcc.Add(bcc);
            mailMessage.IsBodyHtml = isBodyHtml;

            try
            {
                mailClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                string r = ex.Message;
                return false;
            }
        }
        #endregion

       
    }
}
