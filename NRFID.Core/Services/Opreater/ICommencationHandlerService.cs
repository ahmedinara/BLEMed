using BSMediator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Services.Opreater
{
    public interface ICommencationHandlerService
    {
        Task<bool> Send(int commId, MailBody mailBody);
    }
}
