using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Opreater;
using BSMediator.Core.Services.Opreater;
using BSMediator.Core.Services.Opreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Service.Services.Opreater
{
    public class CommencationHandlerService : ICommencationHandlerService
    {
        private readonly IEmailService _emailService;

        private readonly ICommRoleRepository _commRoleRepository;
        public CommencationHandlerService(ICommRoleRepository commRoleRepository, IEmailService emailService)
        {
            _emailService = emailService;
            _commRoleRepository = commRoleRepository;
        }
        public async Task<bool> Send(int commId, MailBody mailBody)
        {
            var comm = await _commRoleRepository.GetCommRoleWithDetialsAsync(commId);
            foreach (var item in comm.commRoleDetials)
            {
                if (item.ApplicationSetting.Value == "Email")
                {
                    await _emailService.SendEmailAsync(mailBody.to, mailBody.Subject, mailBody.body, mailBody.cc, mailBody.bcc, null, true);
                }
                else if (item.ApplicationSetting.Value == "SMS")
                {

                }
            }
            if (comm == null) return false;

            return true;
        }
    }
}
