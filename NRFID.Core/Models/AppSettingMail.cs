using BSMediator.Core.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
   public class AppSettingMail
    {
        [Required]
        [Display(Name = "Host", ResourceType = typeof(Messages))]
        public string Host { get; set; }
        [Required]
        [Display(Name = "Password", ResourceType = typeof(Messages))]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Port", ResourceType = typeof(Messages))]
        public string Port { get; set; }
       
        [EmailAddress]
        [Required]
        [Display(Name = "mailForm", ResourceType = typeof(Messages))]
        public string mailForm { get; set; }
    }
}
