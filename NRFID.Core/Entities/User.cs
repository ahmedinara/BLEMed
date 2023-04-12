using BSMediator.Core.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Entities
{
   public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Messages))]
        public string FristName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Messages))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "MobileNo", ResourceType = typeof(Messages))]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Messages))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(Messages))]
        public string Password { get; set; }

        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedOn", ResourceType = typeof(Messages))]
        public DateTime CreatedOn { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Messages))]
        public bool IsActive { get; set; }
      
        public byte[] PasswordSalt { get; set; }

        public ICollection<UserAction> UserActions { get; set; }

    }
}
