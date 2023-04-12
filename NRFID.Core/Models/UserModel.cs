using BSMediator.Core.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Display(Name = "FristName", ResourceType = typeof(Messages))]
        [Required]
        public string FristName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Messages))]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "MobileNo", ResourceType = typeof(Messages))]
        [Required]
        public string MobileNo { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(Messages))]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Messages))]
        public bool IsActive { get; set; }
    }
}
