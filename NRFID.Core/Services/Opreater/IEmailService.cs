using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Services.Opreation
{
    public interface IEmailService
    {
        #region Send Email General
        Task<bool> SendEmailAsync(string to, string subject, string body, string cc, string bcc, List<Attachment> attachment, bool isBodyHtml = false);
        #endregion
    }
}
