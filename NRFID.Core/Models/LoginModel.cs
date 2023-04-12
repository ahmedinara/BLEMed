using BSMediator.Core.ResourceFiles;
using System.ComponentModel.DataAnnotations;

namespace NFC.Core.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.EmailRequired))]
        [MaxLength(100)]
        [Display(Name = "UserName", ResourceType = typeof(Messages))]
        public string username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
         ErrorMessageResourceName = nameof(Messages.PasswordRequired))]
        [MaxLength(50)]
        [Display(Name = "Password", ResourceType = typeof(Messages))]
        public string password { get; set; }

    }
}
