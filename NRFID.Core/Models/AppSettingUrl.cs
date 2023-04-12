using BSMediator.Core.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
   public class AppSettingUrl
    {
        public AppSettingUrl(string applictionUrl,string port)
        {
            ApplictionUrl = applictionUrl;
            ApplicationPort = port;
        }
        public AppSettingUrl()
        {

        }
        [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.EmailRequired))]
        [Display(Name = "Url", ResourceType = typeof(Messages))]
        public string ApplictionUrl { get; set; }
        [Required]
        [Display(Name = "Port", ResourceType = typeof(Messages))]
        public string ApplicationPort { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.UserNameRequired))]
        [Display(Name = "UserName", ResourceType = typeof(Messages))]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.PasswordRequired))]
        [Display(Name = "Password", ResourceType = typeof(Messages))]
        public string Password { get; set; }
    }
}
