using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
    public class MailBody
    {
        public string to { get; set; }
        public string Subject { get; set; }
        public string body { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
